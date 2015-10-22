(function (jqObj) {
	jqObj.extend(jqObj, {
		
		messageProcess: function (jsonText, funcOk, funcNoOk) {
			var msgObj = null;
			
			if (typeof (jsonText) == "string") {
				try {
					
					msgObj = eval(jsonText);
				} catch (e) {
					try {
						msgObj = eval("(" + jsonText + ")");
					} catch (e1) {
						alert("format error: must be json format.");
					}
				}
			}
			else {
				if (jsonText.Status) {
					msgObj = jsonText;
				}
			}
			
			function doesMsgBoxExsit() {
				return $.msgBoxObj && $.msgBoxObj.showOK && $.msgBoxObj.showOK instanceof Function;
			}
			function invokeFunc(func) {
				if (func && func instanceof Function) {
					func(msgObj);
				}
				else {
					if (typeof (msgObj.BackUrl) == "string" && msgObj.BackUrl) {
						if (window.top == window)
							window.location = msgObj.BackUrl;
						else window.top.location = msgObj.BackUrl;
					}
				}
			}

			switch (msgObj.Status)
			{
				case 1:
				    if (typeof (msgObj.BackUrl) == "string" && msgObj.BackUrl) {
				        if (window.top == window)
				            window.location = msgObj.BackUrl;
				        else window.top.location = msgObj.BackUrl;
				    }
					break;
			    case 2://LoginFailed
					if (doesMsgBoxExsit()) {
						$.msgBoxObj.showFailed(msgObj.Msg, function () { invokeFunc(funcNoOk); });
					}
					break;
			    case 3://NotLogin
					if (doesMsgBoxExsit()) {
						$.msgBoxObj.showInfo(msgObj.Msg, function () { invokeFunc(); });
					}
					break;
			    case 4://NoPermission
					if (doesMsgBoxExsit()) {
						$.msgBoxObj.showInfo(msgObj.Msg, function () { invokeFunc(); });
					}
					break;
			    case 5://OperationSuccess
					if (doesMsgBoxExsit()) {
						$.msgBoxObj.showFailed(msgObj.Msg, function () {
							alert(msgObj.Msg);
						});
					}
					break;
			    case 6://OperationFailed
			        
			        break;
			    case 7://OtherError
			        
			        break;
			}
		}
	});
})(window.$);