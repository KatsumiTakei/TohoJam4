 mergeInto(LibraryManager.library, {
 
 
  GetRotator: function (thisGamObjectName, str) {
                             var url = Pointer_stringify(str);
                             var goname = Pointer_stringify(thisGamObjectName);
 
                             document.write = function(data) {
                                                             SendMessage(goname, "CathData", data);
                                                             }

                             function jsonpCallback(data){};
 
                             var script = document.createElement('script');
                             script.src = url+"?callback=jsonpCallback";
                             document.getElementsByTagName('head')[0].appendChild(script);
 
 
  },

  OpenPopUp: function (AdScript) {
  
           var iframeurl = Pointer_stringify(AdScript);
           var iframe;
           if (iframe==null) {iframe = document.createElement('iframe');}
           if (window.location.protocol == "https:") {var iframeurl = iframeurl.replace("http:", "https:");}

           var closebcode = "var closefr = window.parent.document.getElementById(\'adiframe\'); closefr.parentNode.removeChild(window.parent.document.getElementById(\'adiframe\'));";
		   
           var closebtton = "<a href=\"JavaScript:"+ closebcode + closebcode +"\">CLOSE AD[X]</a>";
           
	   var styleup = ".topright {position: absolute;top: 8px;right: 16px;font-size: 18px; background-color:white;}";
           var stylecenter = ".center {text-align:center; vertical-align:middle;}";
           var html = "<!doctype html><html lang=\"en-us\"><head><style>" + styleup + stylecenter + "</style></head><body leftmargin = 0 topmargin = 0><div class=\"center\">" + iframeurl + "</div><div class=\"topright\">" + closebtton + "</div></body></html>";

document.body.appendChild(iframe);

iframe.id = "adiframe";
iframe.style.position = "absolute";
iframe.style.zIndex = 2;
iframe.style.left = "25%";
iframe.style.top = "15%";
iframe.style.width = "512px";
iframe.style.height = "512px";
iframe.style.frameBorder = "1";
iframe.style.backgroundColor = "black";
iframe.style.scrolling = "no";

iframe.contentWindow.document.open();
iframe.contentWindow.document.write(html);
iframe.contentWindow.document.close();
},
 
   LinkAndPic: function (Link,Pic) {
    var PicUrl = Pointer_stringify(Pic);
    var PicLink = Pointer_stringify(Link);
	
    var iframeurl = "<a href=" + PicLink + "><img src=" + PicUrl +"></a>";
    var iframe;
    if (iframe==null) {iframe = document.createElement('iframe');}

    var closebtton = "<a href=\"JavaScript: var closefr = window.parent.document.getElementById(\'adiframe\'); closefr.parentNode.removeChild(window.parent.document.getElementById(\'adiframe\'));\">CLOSE AD[X]</a>";
	
    var styleup = ".topright {position: absolute;top: 8px;right: 16px;font-size: 18px; background-color:white;}";
    var stylecenter = ".center {text-align:center; vertical-align:middle;}";
    var html = "<!doctype html><html lang=\"en-us\"><head><style>" + styleup + stylecenter + "</style></head><body leftmargin = 0 topmargin = 0><div class=\"center\">" + iframeurl + "</div><div class=\"topright\">" + closebtton + "</div></body></html>";
 
    document.body.appendChild(iframe);

   iframe.id = "adiframe";
   iframe.style.position = "absolute";
   iframe.style.zIndex = 2;
   iframe.style.left = "25%";
   iframe.style.top = "15%";
   iframe.style.width = "512px";
   iframe.style.height = "512px";
   iframe.style.frameBorder = "1";
   iframe.style.backgroundColor = "black";
   iframe.style.scrolling = "no";

   iframe.contentWindow.document.open();
   iframe.contentWindow.document.write(html);
   iframe.contentWindow.document.close();
},
 
});
