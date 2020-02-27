using SetupValidator.Models;
using System.Collections.Generic;

namespace SetupValidator.Abstract
{
    public interface INormalRepository
    {
        IEnumerable<LotData> LotDatas();
    }
}