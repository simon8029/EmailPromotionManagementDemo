using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simon8029.EMPDemo.Model.ModelsForEasyUI
{
    /// <summary>
    /// EasyUI 的数控件 的 节点数据模型
    /// </summary>
    public class EasyUIModel_MenuTreeNode
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 节点文本
        /// </summary>
        public string text { get; set; }

        //public string iconCls { get; set; }

        /// <summary>
        /// 节点状态，'open' 或 'closed'，默认：'open'
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 表示该节点是否被选中
        /// </summary>
        public bool @checked { get; set; }

        /// <summary>
        /// 被添加到节点的自定义属性
        /// </summary>
        public object attributes { get; set; }

        /// <summary>
        /// 子节点集合：一个节点数组声明了若干节点
        /// </summary>
        public List<EasyUIModel_MenuTreeNode> children { get; set; }
    }
}
