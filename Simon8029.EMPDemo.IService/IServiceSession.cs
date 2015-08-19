
 
namespace Simon8029.EMPDemo.IService
{
    using System;
    public partial interface IServiceSession
    {
        IService.IEM_CampaignInstancesService EM_CampaignInstancesService 
    	{ 
    		get;
    	}
    
        IService.IEM_CampaignsService EM_CampaignsService 
    	{ 
    		get;
    	}
    
        IService.IEM_EmailInstancesService EM_EmailInstancesService 
    	{ 
    		get;
    	}
    
        IService.IEM_EmailTemplatesService EM_EmailTemplatesService 
    	{ 
    		get;
    	}
    
        IService.IEM_EmailTemplateTypesService EM_EmailTemplateTypesService 
    	{ 
    		get;
    	}
    
        IService.IEM_EventsService EM_EventsService 
    	{ 
    		get;
    	}
    
        IService.IEM_LeadsService EM_LeadsService 
    	{ 
    		get;
    	}
    
        IService.IDepartmentService DepartmentService 
    	{ 
    		get;
    	}
    
        IService.IEmployeeService EmployeeService 
    	{ 
    		get;
    	}
    
        IService.IemployeeRoleRelationshipService employeeRoleRelationshipService 
    	{ 
    		get;
    	}
    
        IService.IPermissionService PermissionService 
    	{ 
    		get;
    	}
    
        IService.IRoleService RoleService 
    	{ 
    		get;
    	}
    
        IService.IRolePermissionRelationshipService RolePermissionRelationshipService 
    	{ 
    		get;
    	}
    
        IService.IVipPermissionService VipPermissionService 
    	{ 
    		get;
    	}
    
    }
}
