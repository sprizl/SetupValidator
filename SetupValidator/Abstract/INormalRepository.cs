using SetupValidator.Models;
using System.Collections.Generic;

namespace SetupValidator.Abstract
{
    public interface INormalRepository
    {
        IEnumerable<LotData> LotDatas();
        IEnumerable<SetupData> SetupDatas(string mcNo);

        IEnumerable<Bom> BomDatas(string packageName, string deviceName, string testerType, string testFlow, string pcMain);
    }
}