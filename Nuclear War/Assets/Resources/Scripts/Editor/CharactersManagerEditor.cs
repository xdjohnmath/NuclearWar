using UnityEngine;
using System.Collections;
using UnityEditor;

//Editar esse Scritp quando tivermos valores certos no game design, viu Adriel????
[CustomEditor (typeof(CharactersManager))]
public class CharactersManagerEditor : Editor {

    public override void OnInspectorGUI () {

        CharactersManager cm = (CharactersManager) target;
        cm.name = EditorGUILayout.TextField ("Nome ", cm.name);
        cm.goodOrEvil = (GoodOrEvil) EditorGUILayout.EnumPopup ("Organela ou Virus ", cm.goodOrEvil);
        cm.type = (creatureAttackType)EditorGUILayout.EnumPopup ("Tipo de Ataque ", cm.type);
        cm.creatureLife = EditorGUILayout.IntField ("Vida", cm.creatureLife);
        cm.creatureCost = EditorGUILayout.IntField ("Custo", cm.creatureCost);
        cm.creatureAttackRange = EditorGUILayout.IntField ("Tamanho do RayCast ", cm.creatureAttackRange);
        cm.creatureAttack = EditorGUILayout.IntField ("Ataque", cm.creatureAttack);
        cm.creatureIniciative = EditorGUILayout.IntField ("Iniciativa", cm.creatureIniciative);
        cm.creatureSpeed = EditorGUILayout.FloatField ("Velocidade", cm.creatureSpeed);

        switch (cm.type) {
            case creatureAttackType.Tank:
            break;
            case creatureAttackType.LowRange:
                cm.pushedBackForce = EditorGUILayout.FloatField ("Força do Empurrão ", cm.pushedBackForce);
            break;
            case creatureAttackType.HighRange:
                cm.shotSpeed = EditorGUILayout.FloatField ("Velocidade do Tiro ", cm.shotSpeed);
                bool allowSceneObjects = !EditorUtility.IsPersistent (target);
                cm.shot = (GameObject) EditorGUILayout.ObjectField ("Prefab do Tiro ", cm.shot, typeof (GameObject), allowSceneObjects);
            break;
            default:
            break;
        }

        EditorUtility.SetDirty (target);
    }
}
