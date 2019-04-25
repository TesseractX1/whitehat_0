namespace Whitehat.ObjectPools{

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class ObjectPool : MonoBehaviour {
		[SerializeField]private Stack<GameObject> pool;
		[SerializeField]private GameObject prefab;
		[SerializeField]private int startInit;
		public int number=0;

		public GameObject Prefab { get { return prefab; } private set { } }
		public Stack<GameObject> Pool { get { return pool; } private set { } }

		public GameObject UseAndInit(Vector3 position, Vector3 eulerAngles, Transform transform=null){
			if (pool.Count<=0) {
				AddToPool ();
			}
			GameObject obj = pool.Peek ();
			obj.transform.position = position;
			obj.transform.eulerAngles = eulerAngles;
			obj.transform.parent = null;
			obj.SetActive (true);
			obj.GetComponent<PoolObject> ().Start ();
			return pool.Pop();
		}

		public void RecycleObject(GameObject obj){
            obj.GetComponent<PoolObject>().OnDestroy();
			obj.SetActive (false);
			obj.transform.parent = transform;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localEulerAngles = Vector3.zero;
			pool.Push (obj);
		}

		private GameObject AddToPool(){
			GameObject obj=GameObject.Instantiate (prefab, transform);
			obj.SetActive (false);
			pool.Push (obj);
			obj.GetComponent<PoolObject> ().SetPool (this);
			number++;
			return obj;
		}
		
		// Update is called once per frame
		void Start () {
			pool = new Stack<GameObject> ();
			for (int i = 0; i < startInit; i++) {
				AddToPool ();
			}
		}
	}

}
