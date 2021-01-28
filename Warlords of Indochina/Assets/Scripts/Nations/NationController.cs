using Economy;
using UnityEngine;

namespace Player
{
	public abstract class NationController : MonoBehaviour
	{
		public string NationId { get; protected set; }
		public ResourceManagemnt ResourceManagement { get; protected set; }
		
		
	}
}