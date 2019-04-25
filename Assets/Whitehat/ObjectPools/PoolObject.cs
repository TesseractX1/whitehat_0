namespace Whitehat.ObjectPools{

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public interface PoolObject {
		ObjectPool GetPool();
		void SetPool (ObjectPool pool);

		void Start();
        void OnDestroy();
		//void SetSize(float size);
	}

}