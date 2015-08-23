using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Model.ModelsForEasyUI;
using Simon8029.EMPDemo.WebApp.Controllers;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Controllers
{
    public class ManageController : BaseController
    {
        // GET: Admin/Manage
        public ActionResult Index()
        {
            return View();
        }

      
        public ActionResult GetMenuTree()
        {
            //1.从登录用户的 Session中 查询 菜单类型的权限集合
            var menuList = OperationContext.CurrentUserPermissions.FindAll(o => o.permissionOperationType == 1 && o.permissionIsShow == true);
            //2.将 菜单权限集合 转成 TreeNode 节点 树结构
            //2.1创建根节点
            EasyUIModel_MenuTreeNode rootNode = OperationContext.ServiceSession.PermissionService.Get(o => o.permissionParentID == 0).SingleOrDefault().ToMenuTreeNode();
            //2.2根据根节点 对应 的 权限id，查询其所有的子节点
            rootNode.children = GetChildNodes(menuList, rootNode.id);

            var jsSerializer = new JavaScriptSerializer();
            var rootNodeInJs = jsSerializer.Serialize(rootNode);
            return Content("[" + rootNodeInJs + "]");
        }

        #region 递归 生成 子节点 集合
        /// <summary>
        /// 递归 生成 子节点 集合
        /// </summary>
        /// <param name="listPer"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<EasyUIModel_MenuTreeNode> GetChildNodes(List<Permission> listPer, int parentId)
        {
            List<EasyUIModel_MenuTreeNode> childNodes = null;
            //循环权限集合 查找 子权限
            foreach (Permission per in listPer)
            {
                //查找到子权限
                if (per.permissionParentID == parentId)
                {
                    //实例化 子节点集合
                    if (childNodes == null) childNodes = new List<EasyUIModel_MenuTreeNode>();
                    //将子权限 转成 子节点
                    EasyUIModel_MenuTreeNode childNode = per.ToMenuTreeNode();
                    //将子节点 加入 子节点集合
                    childNodes.Add(childNode);
                    //递归为 子节点 查找 子节点集合
                    childNode.children = GetChildNodes(listPer, childNode.id);
                }
            }
            return childNodes;
        }
        #endregion
    }
}