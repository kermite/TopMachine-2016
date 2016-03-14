GridClass = function (gameController, gameState, playGroundId, config, clickable) {
    this.gameController = gameController; 
    this.gameState = gameState;
    this.clickable = false;
    // Contenant HTML de la grille
    this.playGroundid = playGroundId;
    // Configuration de la grille chargée par le controleur
    this.config = config;
    this.currentFontSize = "10px"
}

GridClass.prototype = {
    removeHandlers: function () {
        var table = $(this.playGroundid).find("#grid table");
        table.find("td").unbind('click');
    },
    init: function (clickable) {
        this.removeHandlers();
        this.clickable = clickable;
        this.drawGrid();
    },
    drawGrid: function () {
        // On réinitialise la grille en créant une nouvelle table!
        var grid = $(this.playGroundid).find("#grid");
        grid.html("");
        grid.append("<table cellpadding='0' cellspacing='0' border='0'><tbody></tbody></table>");
        var table = grid.find("tbody");

        for (var x = 0; x < this.config.sizeV + 2; x++) {
            table.append("<tr></tr>");
        }

        for (var x = 0; x < this.config.sizeH + 2; x++) {
            table.find("tr").append("<td></td>");
        }


        // on assigne l'objet a une variable pour y accéder dans les fonctions dynamiques suivantes
        var self = this;

        // la class par défaut de chaque case est "cell", on y ajoute un espace fixe.
        table.find("td").addClass("cell");
        table.find("td").html("");

        // On assigne la classe border aux cellules affichant les coordonnées
        // On profite des capacités de jquery pour faire des requêtes sur le dom
        // pour effectuer la tâche
        table.find("tr:eq(0) td:gt(0)").each(function (i, e) {
            $(e).addClass("border");
            $(e).text(self.config.coordV[e.cellIndex - 1]);
        });

        table.find("tr:last-child td:gt(0)").each(function (i, e) {
            $(e).addClass("border");
            $(e).text(self.config.coordV[e.cellIndex - 1]);
        });

        table.find("tr:gt(0)").find("td:eq(0)").each(function (i, e) {
            $(e).addClass("border");
            var ri = $(e).parent()[0].rowIndex;
            $(e).text(self.config.coordH[ri - 1]);
        });

        table.find("tr:gt(0) td:last-child").each(function (i, e) {
            $(e).addClass("border");
            var ri = $(e).parent()[0].rowIndex;
            $(e).text(self.config.coordH[ri - 1]);
        });

        // Cette boucle va définir la classe supplémentaire à utiliser pour les cases multiplicatrices
        // la classe est "cell"+ le multiplicateur défini.
        for (var x = 1; x <= this.config.sizeH; x++) {
            for (var y = 1; y <= this.config.sizeV; y++) {
                var multiplier = this.gridGetCellColor(x - 1, y - 1);
                var className = "cell" + multiplier;
                table.find("tr:eq(" + y + ")").find("td:eq(" + x + ")")
                            .addClass(className)
                            .attr("multiplier", multiplier);
            }
        }

        // Si on est en mode jeu, alors il est possible de cliquer sur la grille
        // LE click est renvoyé au controleur de jeu.
        if (this.clickable) {
            table.find("td").click(
                function () {
                    self.gameController.clickGrid(this.parentNode.rowIndex, this.cellIndex);
                }
            );
        }

        // On définit la taille des cases
        this.resizeGrid();

    },
    /*
    Est appelé à l'initialisation et par les événements de redimensionnement de la fenêtre ou 
    du layout manager.
    */
    resizeGrid: function () {
        // La taille de la grille est dynamique et est dépendante du container principal.
        // Si pour une raison ou une autre on change la présentation, ou le container
        // est redimensionné on peut redéfinir la taille des cases.
        var grid = $(this.playGroundid).find("#grid");
        var table = grid.find("table");

        // On calcule donc la taille du div afin de calculer la taille des cases
        var size = grid.width();
        var h = grid.height();

        // On prend la taille la plus petite et on calcule la taille minimal d'une case
        if (size > h) size = h;

        size = Math.round(size / (this.config.sizeH + 2));

        table.find("td").width(size - 2);
        table.find("td").height(size - 2);

        // On change la taille de la police à 60% de la taille de la case pour toutes 
        // Les cases de jeu => la class commence par cell.
        this.currentFontSize = (Math.round(size * 60 / 100)) + "px";
        table.find("td[class^='cell']").css('font-size', this.currentFontSize);

        // Comme la taille de la grille ne correspond pas forcément à la taille de son parent
        // Nous recentrons la grille dans celui-ci        
        var table = grid.find("table");
        var left = Math.round((grid.width() - table.width()) / 2);
        table.css("margin-left", left + "px");

        var top = Math.round((grid.height() - table.height()) / 2);
        table.css("margin-top", top + "px");

    },
    // Réinitialise la grille en vidant toutes les lettres 
    clearGrid: function () {
        var table = $(this.playGroundid).find("#grid table tbody");
        for (var x = 1; x <= this.config.sizeH; x++) {
            for (var y = 1; y <= this.config.sizeV; y++) {
                // Une particularité de Jquery est de pouvoir appeler
                // des fonctions à la queue. 
                // Un exemple ici : table.find.find -> Sélectionne des éléments du DOM
                // removeClass - removeclass - text seront exécutés sur la sélection courante.
                table.find("tr:eq(" + y + ")").find("td:eq(" + x + ")")
                    .removeClass("tile")
                    .removeClass("tileJoker")
                    .text("");
            }
        }

    },
    setGame: function (rounds, roundNumber) {
        this.clearGrid();
        for (var x = 0; x < roundNumber; x++) {
            this.gridSetWord(rounds[x].Word, rounds[x].Direction);
        }
    },
    setWord: function (round, animate) {
        this.gridSetWord(round.Word, round.Direction, animate);
    },
    // Cette fonction parcourt le tableau des cases multiplicatrices et retourne le multiplicateur
    // d'une case selon ses coordonnées x, y
    gridGetCellColor: function (x, y) {
        var array = this.config.multipliers;
        for (var xx = 0; xx < array.length; xx += 3) {
            if (array[xx] == x && array[xx + 1] == y)
                return array[xx + 2];
        }
        return 0;
    },
    // Place un mot sur la grille selon sa position
    gridSetWord: function (Word, Position, animate) {
        var rowNdx = Position.substring(0, 1);
        var col = "0";
        var len = Position.length;
        var dir = 0;

        if (rowNdx >= "A" && rowNdx <= "Z") {
            dir = 1;
            col = Position.substring(1, len);

        } else {
            rowNdx = Position.substring(len - 1, len);
            col = Position.substring(0, len - 1);
            dir = 0;
        }

        for (xx = 0; xx < 15; xx++) {
            if (rowNdx == this.config.coordH[xx]) {
                rowNdx = xx + 1;
                break;
            }
        }

        if (dir == 0) {
            this.gridSetWordVertical(Word, rowNdx, col)
        } else {
            this.gridSetWordHorizontal(Word, rowNdx, col)
        }

        var table = $(this.playGroundid).find("#grid table tbody");

        if (animate) {
            var self = this;
            // table.find("[toAnim='1']").fadeIn("slow");
            table.find("[toAnim='1']").animate({ fontSize: 2 }, "slow");
            table.find("[toAnim='1']").animate({ fontSize: self.currentFontSize }, "slow");
            // table.find("[toAnim='1']").animate({ "height": "toggle", "opacity": "toggle" }, "slow");

        }

        table.find("[toAnim='1']").removeAttr("toAnim");
    },
    /*
    Place un mot verticalement sur la grille
    */
    gridSetWordVertical: function (Word, row, col) {
        var table = $(this.playGroundid).find("#grid table tbody");

        var len = Word.length;

        for (var xx = 0; xx < len; xx++) {

            var cell = table.find("tr:eq(" + row + ")").find("td:eq(" + col + ")");

            if (Word.substring(xx, xx + 1) >= "A" && Word.substring(xx, xx + 1) <= "Z") {
                cell.addClass("tile");
                ;
            } else {
                cell.addClass("tileJoker");
            }

            if ($(cell).text() == "") {
                $(cell).text(Word.substring(xx, xx + 1).toUpperCase());
                $(cell).attr("toAnim", "1");
            }

            row++;
        }
    },
    /*
    Place un mot horizontalement sur la grille
    */
    gridSetWordHorizontal: function (Word, row, col) {
        var table = $(this.playGroundid).find("#grid table tbody");

        var len = Word.length;

        for (var xx = 0; xx < len; xx++) {

            var cell = table.find("tr:eq(" + row + ")").find("td:eq(" + col + ")");

            if (Word.substring(xx, xx + 1) >= "A" && Word.substring(xx, xx + 1) <= "Z") {
                cell.addClass("tile");
                ;
            } else {
                cell.addClass("tileJoker");
            }

            if ($(cell).text() == "") {
                $(cell).text(Word.substring(xx, xx + 1).toUpperCase());
                $(cell).attr("toAnim", "1");
            }

            col++;
        }
    }
}