using Economy;
using UnityEngine;

namespace Nations
{
	public abstract class NationController : MonoBehaviour
	{
		public string NationId { get; protected set; }
		public ResourceManagemnt ResourceManagement { get; protected set; }
		
		
	}
}