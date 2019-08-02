using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SmallManagerSpace.Resources
{

    /// <summary>
    /// 存放用户自定义数据
    /// </summary>
    public class DefineEntity
    {
        public string type { get; set; }
        public string name { get; set; }
    }
    
    public class DefineEntityFuncion
    {
        public List<DefineEntity> CreateDefineEntity()
        {
            List<DefineEntity> userDefineDatas = new List<DefineEntity>();
            userDefineDatas.Add(new DefineEntity() { type = "OTN_USER_B_TYPE_INFO", name = "OTN_USER_B_TYPE_INFO_VAR" });
            userDefineDatas.Add(new DefineEntity() { type = "OTN_USER_BOARD_INFO", name = "OTN_USER_BOARD_INFO_VAR[board_num]" });
            userDefineDatas.Add(new DefineEntity() { type = "OTN_USER_PORT_INFO", name = "OTN_USER_PORT_INFO_VAR[board_num]" });
            userDefineDatas.Add(new DefineEntity() { type = "OTN_USER_FPGA_INFO", name = " OTN_USER_FPGA_INFO_VAR[board_num]" });
            userDefineDatas.Add(new DefineEntity() { type = "OTN_USER_BOARD_ALM_INFO", name = "OTN_USER_BOARD_ALM_INFO_VAR[board_num]" });
            userDefineDatas.Add(new DefineEntity() { type = "OTN_USER_FRAMER_INFO", name = "OTN_USER_FRAMER_INFO_VAR[board_num]" });
            userDefineDatas.Add(new DefineEntity() { type = "OTN_USER_REG_FUNC_NAME", name = "OTN_USER_REG_FUNC_NAME_VAR[board_num]" });
            userDefineDatas.Add(new DefineEntity() { type = "OTN_USER_SSM_INFO", name = "OTN_USER_SSM_INFO_VAR[board_num]" });
            return userDefineDatas;
        }
    }

}
