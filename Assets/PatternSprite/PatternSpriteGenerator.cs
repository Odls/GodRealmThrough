using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class PatternSpriteGenerator : EditorWindow{
	static PatternSpriteGenerator window;
	[MenuItem("Window/PatternSpriteGenerator")]
	static void Init() {
		if(window == null) {
			window = GetWindow<PatternSpriteGenerator>();
		}
		window.Show();
	}
	Texture2D originalTex;
	Texture2D patternTex;
	Sprite originalPattern;
	Vector2 patternGrid;

	void OnGUI() {
		originalTex = EditorGUILayout.ObjectField("Original Texture", originalTex, typeof(Texture2D), false) as Texture2D;
		originalPattern = EditorGUILayout.ObjectField("Original Pattern", originalPattern, typeof(Sprite), false) as Sprite;
		patternGrid = EditorGUILayout.Vector2Field("pattern Grid", patternGrid);

		if (GUILayout.Button("Generate")) {
			Generate();
		}
	}

	void Generate() {
		Dictionary<Color32, Color32> _remapDict = new Dictionary<Color32, Color32>();

		// Get Pattern Colors
		Color32[] _patternColors = originalPattern.texture.GetPixels32();
		Rect _rect = originalPattern.rect;
		for (int y = 0; y < patternGrid.y; y++) {
			int _yw = (int)(
				(_rect.y + (y / patternGrid.y)* _rect.height) *
				originalPattern.texture.width
			);
			for (int x = 0; x < patternGrid.x; x++) {
				Color32 _targetColor = _patternColors[
					(int)(_rect.x + (x / patternGrid.x) * _rect.width) + _yw
				];
				if(_targetColor.a > 0) {
					_remapDict.Add(
						_targetColor,
						new Color(x/patternGrid.x, y/patternGrid.y, 0.5f)
					);
				}
			}
		}

		// Replace Sprite Colors
		Color32[] _colors = originalTex.GetPixels32();
		Color32 _emptyColor = new Color(0.5f, 0.5f, 1f, 0);
		for (int f = 0; f < _colors.Length; f++) {
			Color32 _targetColor;
			if (_remapDict.TryGetValue(_colors[f], out _targetColor)) {
				_colors[f] = _targetColor;
			} else {
				_colors[f] = _emptyColor;
			}
		}

		// Add Original Pattern
		Object[] _objs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(originalTex));
		foreach(Object _obj in _objs) {
			Sprite _sprite = _obj as Sprite;
			if (_sprite) {
				_rect = _sprite.rect;
				for (int y = (int)_rect.y; y < (_rect.y + _rect.height); y++) {
					int _yw = (int)(y * originalPattern.texture.width);
					for (int x = (int)_rect.x; x < (_rect.x + _rect.width); x++) {
						int f = x + _yw;
						Color32 _targetColor = _patternColors[f];
						_colors[f] = _targetColor;
					}
				}
			}
		}

		// Save Texture2D
		patternTex = new Texture2D(originalTex.width, originalTex.height, TextureFormat.ARGB32, false);
		patternTex.SetPixels32(_colors);
		string _assetPath = AssetDatabase.GetAssetPath(originalTex);
		string _ext = Path.GetExtension(_assetPath);
		string _path = Path.GetFullPath(_assetPath);
		_path = _path.Replace(_ext, "_pattern.png");
		File.WriteAllBytes(_path, patternTex.EncodeToPNG());
		_assetPath = _assetPath.Replace(_ext, "_pattern.png");
		AssetDatabase.ImportAsset(_assetPath);
	}
}
