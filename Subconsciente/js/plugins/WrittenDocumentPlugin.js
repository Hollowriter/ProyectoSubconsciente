// ===========================================================================
//	WrittenDocumentPlugin.js
// ===========================================================================

/*:
* @author Cosito
* @plugindesc A plugin to read and write files using Node.js
*/

(function(){
	function setup(){
		function WrittenDocumentFS(){

		}

		WrittenDocumentFS.fs = require("fs");

		WrittenDocumentFS.writeFile = function (filePath, fileName, data){
			filePath = this.createPath("/" + filePath + "/");
			this.fs.writeFileSync(filePath + fileName, data);
			console.log("Wrote file: " + fileName);
		};

		WrittenDocumentFS.readFile = function(filePath, fileName){
			filePath = this.createPath("/" + filePath + "/");
			console.log("Read File: ", fileName);
			return this.fs.readFileSync(filePath + fileName, "utf8");
		};

		WrittenDocumentFS.createPath = function (thyPath){
			thyPath = (Utils.isNwjs() && Utils.isOptionValid("test")) ? thyPath : "//" + thyPath;
			var path = window.location.pathname.replace(/(\/|)\/[^\/]*$/, thyPath);
			if (path.match(/^\/([A-Z]\:)/)){
				path = path.slice(1);
			}
			path = decodeURIComponent(path);
			return path;
		};

		window.WrittenDocumentFS = WrittenDocumentFS;
	}

	setup();

})();