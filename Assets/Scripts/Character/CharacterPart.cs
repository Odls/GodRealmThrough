using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPart : MonoBehaviour{
	protected Character character;
	internal virtual void Init(Character p_character) {
		character = p_character;
	}
}
