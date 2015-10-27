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
            //1. get user's menu type permissions from session
            var menuList = OperationContext.CurrentUserPermissions.FindAll(o => o.permissionOperationType == 1 && o.permissionIsShow == true);
            //2. convert menu permisison to treeNode
            //2.1 create root node
            EasyUIModel_MenuTreeNode rootNode = OperationContext.ServiceSession.PermissionService.Get(o => o.permissionParentID == 0).SingleOrDefault().ToMenuTreeNode();
            //2.2 get all child node by id
            rootNode.children = GetChildNodes(menuList, rootNode.id);

            var jsSerializer = new JavaScriptSerializer();
            var rootNodeInJs = jsSerializer.Serialize(rootNode);
            return Content("[" + rootNodeInJs + "]");
        }

        #region generate child nodes
        /// <summary>
        /// generate child nodes
        /// </summary>
        /// <param name="listPer"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<EasyUIModel_MenuTreeNode> GetChildNodes(List<Permission> listPer, int parentId)
        {
            List<EasyUIModel_MenuTreeNode> childNodes = null;
            
            foreach (Permission per in listPer)
            {
                if (per.permissionParentID == parentId)
                {
                    if (childNodes == null) childNodes = new List<EasyUIModel_MenuTreeNode>();
                    EasyUIModel_MenuTreeNode childNode = per.ToMenuTreeNode();
                    childNodes.Add(childNode);
                    childNode.children = GetChildNodes(listPer, childNode.id);
                }
            }
            return childNodes;
        }
        #endregion
    }
}