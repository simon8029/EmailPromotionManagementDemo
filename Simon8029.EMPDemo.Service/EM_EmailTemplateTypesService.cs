// **********************************************************************
// SOLUTION: 
// PROJECT: 
// FILE NAME: 
// CREATED ON 8/19/2015
//   
// Copyright (C) 2015 Simon8029
// All rights reserved.
// 
// This software may be modified and distributed under the terms
// of the BSD license.  See the LICENSE file for details.
// **********************************************************************

using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.IService;
using Simon8029.EMPDemo.IRepository;
namespace Simon8029.EMPDemo.Service{
    public partial class EM_EmailTemplateTypesService : BaseService<EM_EmailTemplateTypes>,IEM_EmailTemplateTypesService
    {
    	public override void SetIBaseRepository()
    	{
    		IbaseRepository=DbSession.EM_EmailTemplateTypesRepository;
    	}
    }
}
