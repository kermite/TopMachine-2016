


/* Cette classe est le contrôleur de l'application 
Il contient tous les éléments nécessaires pour piloter l'application et ses
différents éléments
*/

GameControllerClass = function (mode, gameId) {
    // Le chronomètre
    this.chrono = null; 
    // La grilled
    this.grid = null;
    // Le chevalet 
    this.rack = null;
    // L'historique
    this.history = null;
    // Les commentaires
    this.comments = null;
    // Les classements 
    this.rankings = null;
    // LE contrôleur de tours
    this.roundControl = null;
    // L'affichage du top
    this.topInfo = null;

    // L'état de la partie
    this.gameState = new GameStateClass(mode, gameId);


    // L'objet qui charge la partie... Ici Local/GameLoader est utilisé.
    // Permet de charger des parties localement. Mais d'autres classes
    // doivent être créees pour une application client / serveur
    this.gameState.gameLoader = null;
};

GameControllerClass.prototype = {
    init: function () {
        // On crée un chargeur de partie par défaut   
        this.gameState.gameLoader = new GameLoader(this, this.gameState);
        // On crée une configuration par défaut
        this.gameState.gameLoader.init({ gameId: null, first: true });
    },
    initContinue: function () {
        var self = this;
        // On crée le chrono
        this.chrono = new ChronoClass("#lblChrono", 60);

        // On crée la grille 
        this.grid = new GridClass(this, this.gameState, "#playGround", this.gameState.gameLoader.config.gridConfig, false);
        this.grid.init(false);

        // On crée le chevalet
        this.rack = new RackClass(this, this.gameState, "#rack");
        this.rack.init();

        // On crée l'historique
        this.history = new HistoryClass(this, this.gameState, "#tabHistory", function (round) { self.setRound(round); });
        this.history.init();

        // On crée le controleur de tours
        this.roundControl = new RoundControlClass(this, this.gameState);

        // on crée le controle d'affichage du top
        this.topInfo = new TopInfoClass(this, this.gameState);

        // on crée le controle de classements
        this.rankings = new RankingClass(this, this.gameState);
        this.rankings.init();

        $("#viewGame").click(function () { self.startGame(null, 0); });
        $("#viewPlayGame").click(function () { self.startGame(null, 1); });

        // Et on place des mots sur la grille pour faire joli.
        this.grid.gridSetWord("BIeNVeNUe", "E3", true);
        this.grid.gridSetWord("SUR", "9G", true);
        this.grid.gridSetWord("WeB.CHRAB", "K6", true);
        this.rack.setRack("WEBCHRAB", true);

        // On crée le controleur de commentaires
        this.comments = new CommentsClass(this.gameState, "#tabComments");
        this.comments.init();

        if (this.gameState.gameId != "") {
            switch (this.gameState.mode) {
                case 0:
                    this.startGame(this.gameState.gameId, 0);
                    break;
            }
        }
    },
    error: function (result) {
        alert("An error occured : " + result.message);
    },
    success: function (result) {
        switch (result.typeMessage) {
            case "proxy_init":
                if (result.params.first) {
                    this.initContinue();
                } else {
                    result.params.callback();
                }
                break;
            case "proxy_gameLoad":
                this.gameLoaded();
                break;
            case "proxy_rankingLoad":
                this.rankings.setRanking(result);
                break;
        }
    },
    loadRanking: function (params) {
        var rank = this.gameState.gameLoader.getRankings(params);
    },
    startGame: function (gameId, mode) {
        // On montre l'indicateur de chargement
        // Plugin jquery.showLoading
        jQuery('#grid').showLoading();

        this.gameState.mode = mode;

        // on charge la partie
        // Un callback renvoie vers la fonction gameLoaded
        this.gameState.gameLoader = new GameLoader(this, this.gameState);
        var self = this;
        var fn = function () { self.gameState.gameLoader.loadGame(true); };
        this.gameState.gameLoader.init({ first: false, gameId: gameId, callback: fn });
    },
    gameLoaded: function (ok) {
        // On recrée la grille 
        this.grid = new GridClass(this, this.gameState, "#playGround", this.gameState.gameLoader.config.gridConfig);
        this.grid.init(this.gameState.mode == 0 ? false : true);

        // On recrée le chevalet
        this.rack = new RackClass(this.gameState, "#rack");
        this.rack.init();

        // On enlève l'indicateur de chargement
        jQuery('#grid').hideLoading();
        if (ok == 0) {
            alert("Une erreur s'est produite. La partie n'a pas pu être chargée.");
            return;
        }

        this.gameState.currentRound = 0;

        // La partie est chargée on efface le contenu actuel de la grille
        this.grid.clearGrid();
        this.rack.clearRack();

        // On affiche l'historique de la partie si et seulement si on est en mode visualisation
        // En mode de jeu, l'historique s'affichera au coup par coup.
        // En mode partiel, si on est pas en mode jeu, il affiche l'historique déjà chargé
        if (this.gameState.mode == 0) {
            this.history.setHistory(this.gameState.gameLoader.getAllRounds());
        }

        this.roundControl.init();
        this.topInfo.init();

        contentLayout.resizeAll();

        this.setRound(this.gameState.currentRound);
    },
    // On affiche la solution 
    setRound: function (round) {
        round = parseInt(round);
        switch (this.gameState.mode) {
            case 0:
            case 1: 
            case 2:
                this.gameState.currentRound = round;
                this.chrono.start(this.gameState.gameLoader.config.gameConfig.chrono);
                this.rack.setRack(this.gameState.gameLoader.getRound(this.gameState.currentRound).Tirage);
                this.grid.setGame(this.gameState.gameLoader.getAllRounds(), this.gameState.currentRound);
                this.comments.setComments(true, this.gameState.gameLoader.getComments(round, 0));
                this.roundControl.setRoundPosition();
                this.roundControl.addHandlers(false);
                return;
        }


    },
    // On affiche seulement la solution du coup actuel
    setSolution: function () {
        this.gameState.currentSolution = this.gameState.gameLoader.getRound(this.gameState.currentRound);
        this.grid.setWord(this.gameState.currentSolution, true);
        this.comments.setComments(false, this.gameState.gameLoader.getComments(this.gameState.currentRound, 2));
        this.topInfo.setSolution(); 
        this.roundControl.addHandlers(true);
    },

    // Hop au retour au coup précédent
    previousRound: function () {
        switch (this.gameState.mode) {
            case 0:
            case 1:
            case 2:
                if (this.gameState.currentRound > 0) {
                    this.gameState.currentRound--;
                    this.setRound(this.gameState.currentRound);
                }
                break;
        }
    },
    // Tour suivant
    nextRound: function () {
        switch (this.gameState.mode) {
            case 0:
            case 1:
            case 2:
                this.setSolution();
                if (this.gameState.currentRound < this.gameState.gameLoader.getTotalRounds() - 1) {
                    this.gameState.currentRound++;
                    this.setRound(this.gameState.currentRound);
                    return;
                }
                if (this.gameState.currentRound == this.gameState.gameLoader.getTotalRounds() - 1) {
                    this.grid.setGame(this.gameState.gameLoader.getAllRounds(), this.gameState.currentRound + 1);
                    this.rack.clearRack();
                }
                break;
        }
    },
    clickGrid: function (row, col) {
        ;
    }
};
