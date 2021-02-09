namespace UnityEngine.Tilemaps.Samples
{
	public class Explosion : MonoBehaviour {

		void Start () 
		{
			Destroy(this.gameObject, 1f);
		}
	}
}
