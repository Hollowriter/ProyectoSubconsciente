/*:
 * @plugindesc MenuDeAcciones
 * @author Hollowriter
 * @help Just push M
 */
(function(){
var _Scene_Menu_create = Scene_Menu.prototype.create;
 Scene_Menu.prototype.create = function() {
 	_Scene_Menu_create.call(this);
	/*this._commandWindow
	this._goldWindow
	this._statusWindow*/
};
})();