﻿/*
 *  TexturePacker Importer
 *  (c) CodeAndWeb GmbH, Saalbaustraße 61, 89233 Neu-Ulm, Germany
 * 
 *  Use this script to import sprite sheets generated with TexturePacker.
 *  For more information see http://www.codeandweb.com/texturepacker/unity
 * 
 *  Thanks to Brendan Campbell for providing a first version of this script!
 *
 */

using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePackerImporter : AssetPostprocessor
{

	static string[] textureExtensions = {
		".png",
		".jpg",
		".jpeg",
		".tiff",
		".tga",
		".bmp"
	};

	/*
	 *  Trigger a texture file re-import each time the .tpsheet file changes (or is manually re-imported)
	 */
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (var asset in importedAssets) {
			if (!Path.GetExtension (asset).Equals (".tpsheet"))
				continue;
			foreach (string ext in textureExtensions) {
				string pathToTexture = Path.ChangeExtension (asset, ext);
				if (File.Exists (pathToTexture)) {
					AssetDatabase.ImportAsset (pathToTexture, ImportAssetOptions.ForceUpdate);
					break;
				}
			}
		}
	}


	/*
	 *  Trigger a sprite sheet update each time the texture file changes (or is manually re-imported)
	 */
	void OnPreprocessTexture ()
	{
		TextureImporter importer = assetImporter as TextureImporter;

		string pathToData = Path.ChangeExtension (assetPath, ".tpsheet");
		if (File.Exists (pathToData)) {
			updateSpriteMetaData (importer, pathToData);
		}
	}

	static void updateSpriteMetaData (TextureImporter importer, string pathToData)
	{
		if (importer.textureType != TextureImporterType.Default) {
			importer.textureType = TextureImporterType.Sprite;
		}
		importer.maxTextureSize = 4096;
		importer.spriteImportMode = SpriteImportMode.Multiple;

		string[] dataFileContent = File.ReadAllLines(pathToData);
		int format = 30302;

		foreach (string row in dataFileContent)
		{
			if (row.StartsWith(":format=")) {
				format = int.Parse(row.Remove(0,8));
			}
		}
		if (format != 30302) {
			EditorUtility.DisplayDialog("Please update TexturePacker Importer", "Your TexturePacker Importer is too old, \nplease load a new version from the asset store!", "Ok");
			return;
		}

		List<SpriteMetaData> metaData = new List<SpriteMetaData> ();
		foreach (string row in dataFileContent) {
			if (string.IsNullOrEmpty (row) || row.StartsWith ("#") || row.StartsWith (":"))
				continue; // comment lines start with #, additional atlas properties with :

			string [] cols = row.Split (';');
			if (cols.Length < 7)
				return; // format error

			SpriteMetaData smd = new SpriteMetaData ();
			smd.name = cols [0].Replace ("/", "-");  // unity has problems with "/" in sprite names...
			float x = float.Parse (cols [1]);
			float y = float.Parse (cols [2]);
			float w = float.Parse (cols [3]);
			float h = float.Parse (cols [4]);
			float px = float.Parse (cols [5]);
			float py = float.Parse (cols [6]);

			smd.rect = new UnityEngine.Rect (x, y, w, h);
			//smd.pivot = new UnityEngine.Vector2 (px, py);

            //if (px == 0 && py == 0)
            //    smd.alignment = (int)UnityEngine.SpriteAlignment.BottomLeft;
            //else if (px == 0.5 && py == 0)
            //    smd.alignment = (int)UnityEngine.SpriteAlignment.BottomCenter;
            //else if (px == 1 && py == 0)
            //    smd.alignment = (int)UnityEngine.SpriteAlignment.BottomRight;
            //else if (px == 0 && py == 0.5)
            //    smd.alignment = (int)UnityEngine.SpriteAlignment.LeftCenter;
            //else if (px == 0.5 && py == 0.5)
            //    smd.alignment = (int)UnityEngine.SpriteAlignment.Center;
            //else if (px == 1 && py == 0.5)
            //    smd.alignment = (int)UnityEngine.SpriteAlignment.RightCenter;
            //else if (px == 0 && py == 1)
            //    smd.alignment = (int)UnityEngine.SpriteAlignment.TopLeft;
            //else if (px == 0.5 && py == 1)
            //    smd.alignment = (int)UnityEngine.SpriteAlignment.TopCenter;
            //else if (px == 1 && py == 1)
            //    smd.alignment = (int)UnityEngine.SpriteAlignment.TopRight;
            //else
            //    smd.alignment = (int)UnityEngine.SpriteAlignment.Custom;

            // Import extisting border
            foreach(var existing in importer.spritesheet)
            {
                if (existing.name == smd.name)
                {
                    smd.border = existing.border;
                    smd.pivot = existing.pivot;
                    smd.alignment = existing.alignment;
                }                    
            }                

			metaData.Add (smd);
		}
                

        importer.spritesheet = metaData.ToArray();   	
	}
}
