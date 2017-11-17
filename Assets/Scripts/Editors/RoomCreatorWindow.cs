using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

namespace Levels.Rooms.RoomCreator
{
    public class RoomCreatorWindow : EditorWindow
    {
        [MenuItem("Window/Room Editor/Room Editor Window")]
        public static void Init()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene("Assets/Scenes/RoomCreator.unity");
                RoomCreatorWindow window = (RoomCreatorWindow)GetWindow(typeof(RoomCreatorWindow));
                window.Show();
            }
        }

        private void OnGUI()
        {
            GameObject gameObject = null;
            gameObject = EditorGUILayout.ObjectField("Open room:", gameObject, typeof(GameObject), false)
                as GameObject;

            if (gameObject != null)
            {
                OpenRoom();
            }

            if (GUILayout.Button("Create room"))
            {
                CreateRoom();
            }

            GameObject prefab = null;
            prefab = EditorGUILayout.ObjectField("Add prefab:", prefab, typeof(GameObject), false)
                as GameObject;

            if (prefab != null)
            {
                LoadPrefab(prefab);
            }

            if (GUILayout.Button("Quantize objects' position"))
            {
                Quantize();
            }

            if (GUILayout.Button("Save"))
            {
                Save();
            }
        }

        private void OpenRoom()
        {

        }

        private void CreateRoom()
        {

        }

        private void LoadPrefab(GameObject prefab)
        {
            Instantiate(prefab);
        }

        private void Quantize()
        {

        }

        private void Save()
        {

        }
    }
}

