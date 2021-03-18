using System.Collections.Generic;
using GlobalDatas;

namespace Provinces
{
	public interface IProvinceFetchedListener
	{ 
		void OnProvincesFetched(List<ProvinceData> provinceDatas);
	}
}