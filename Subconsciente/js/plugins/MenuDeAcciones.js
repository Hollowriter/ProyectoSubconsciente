/*:
 * @plugindesc MenuDeAcciones
 * @author Hollowriter
 * @help Just push M
 */
(function(){
	Scene_Menu.prototype.create = function() {
    	Scene_MenuBase.prototype.create.call(this);
    	this.createCommandWindow();
	};
	Scene_Menu.prototype.start = function() {
    	Scene_MenuBase.prototype.start.call(this);
    	//this._statusWindow.refresh();
	};
	Scene_Menu.prototype.createCommandWindow = function() {
    	this._commandWindow = new Window_MenuCommand(0, 0);
    	this._commandWindow.setHandler('item',      this.commandItem.bind(this));
    	//this._commandWindow.setHandler('skill',     this.commandPersonal.bind(this));
    	//this._commandWindow.setHandler('equip',     this.commandPersonal.bind(this));
    	//this._commandWindow.setHandler('status',    this.commandPersonal.bind(this));
    	//this._commandWindow.setHandler('formation', this.commandFormation.bind(this));
    	//this._commandWindow.setHandler('options',   this.commandOptions.bind(this));
    	this._commandWindow.setHandler('interact',      this.commandInteract.bind(this));
    	this._commandWindow.setHandler('save',      this.commandSave.bind(this));
    	this._commandWindow.setHandler('gameEnd',   this.commandGameEnd.bind(this));
    	this._commandWindow.setHandler('cancel',    this.popScene.bind(this));
    	this.addWindow(this._commandWindow);
	};
	Window_MenuCommand.prototype.addMainCommands = function() {
    	var enabled = this.areMainCommandsEnabled();
    	if (this.needsCommand('item')) {
        	this.addCommand(TextManager.item, 'item', enabled);
    	}
    	/*if (this.needsCommand('skill')) {
        	this.addCommand(TextManager.skill, 'skill', enabled);
    	}
    	if (this.needsCommand('equip')) {
        	this.addCommand(TextManager.equip, 'equip', enabled);
    	}
    	if (this.needsCommand('status')) {
        	this.addCommand(TextManager.status, 'status', enabled);
    	}*/
	};
	Window_MenuCommand.prototype.makeCommandList = function() {
    	this.addMainCommands();
    	this.addInteractCommand();
    	//this.addFormationCommand();
    	//this.addOriginalCommands();
    	//this.addOptionsCommand();
    	this.addSaveCommand();
    	this.addGameEndCommand();
	};
	Scene_Menu.prototype.commandInteract = function (){
		SceneManager.push(Scene_Map);
		/*$gameSwitches.value[0003] = true;*/
		//$gameMessage.add("Psicologo: no hay nada con que interactuar muchacho");
		/*Scene_MenuBase.prototype.stop.call(this);
		this._commandWindow.close();
		this._commandWindow.deactivate();*/
	}
	Window_MenuCommand.prototype.addInteractCommand = function(){
		this.addCommand('Interact', 'interact', true);
	};
})();