using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

using TopMachine.Topping.Common.Interfaces;
using TopMachine.Desktop.Overall;

namespace TopMachine.Topping.Common.Structures
{
    public class GameControllers : IDisposable 
    {
        Collection<IKeyHandler> keyHandlers = new Collection<IKeyHandler>();
        
        IRackManager RackMan;
        IRoundManager roundManager;
        ISearchResult searchResult;
        ISolutionChooser solutionChooser;
        IChevalet chevalet;
        IRoundControl roundCountrol;

        IBoardManager board;
        IHistoryControl historyControl;
        IHistoryControl historyPlayerControl;
        IClassement classementControl;
        IPlayedGameRoundControl playedGameRoundControl;
        //ICommentator commentator;

       /* public ICommentator Commentator
        {
            get { return commentator; }
            set { commentator = value; }
        }*/

        public IRoundControl  RoundControl
        {
            get { return roundCountrol; }
            set { roundCountrol= value; }

        }

        public IGameController GameController
        {
            get;
            set; 
        }
        public IPlayedGameRoundControl PlayedGameRoundControl
        {
            get { return playedGameRoundControl; }
            set { playedGameRoundControl = value; }
        }

        public IClassement ClassementControl
        {
            get { return classementControl; }
            set { classementControl = value; }
        }

        public IHistoryControl HistoryPlayerControl
        {
            get { return historyPlayerControl; }
            set { historyPlayerControl = value; }
        }

        public IHistoryControl HistoryControl
        {
            get { return historyControl; }
            set { historyControl = value; }
        }

        public IBoardManager Board
        {
            get { return board; }
            set { board = value; }
        }



        public IChevalet Chevalet
        {
            get { return chevalet; }
            set { chevalet = value; }
        }


        public Collection<IKeyHandler> KeyHandlers
        {
            get { return keyHandlers; }
            set { keyHandlers = value; }
        }

        public ISolutionChooser SolutionChooser
        {
            get { return solutionChooser; }
            set { solutionChooser = value; }
        }

       /* public IClassement ClassementManager
        {
            get { return classementControl; }
            set {classementControl = value; }
        }*/

        public IRackManager RackManager
        {
            set { this.RackMan = value; }
            get { return this.RackMan; }
        }

        public IRoundManager RoundManager
        {
            get { return roundManager; }
            set { roundManager = value; }
        }

        public ISearchResult SearchResult
        {
            get { return searchResult; }
            set { searchResult = value; }
        }

        public void Dispose()
        {
            

            if (Board != null) Board.ResultControl.Dispose();
            if (Chevalet != null) Chevalet.ResultControl.Dispose();
            if (ClassementControl != null) ClassementControl.ResultControl.Dispose();
            if (HistoryControl != null) HistoryControl.ResultControl.Dispose();
            if (HistoryPlayerControl != null) HistoryPlayerControl.ResultControl.Dispose();
            if (PlayedGameRoundControl != null) PlayedGameRoundControl.ResultControl.Dispose();
            if (RoundControl != null) RoundControl.ResultControl.Dispose();
           // if (GameController != null) GameController.Dispose();
            Board = null;
            Chevalet = null;
            ClassementControl = null;
            GameController = null;
            HistoryControl = null;
            HistoryPlayerControl = null;
            if (keyHandlers != null) keyHandlers.Clear();  
            KeyHandlers = null;
            PlayedGameRoundControl = null;
            RackManager = null;
            RoundControl = null;
            RoundManager = null;
            SearchResult = null;
            SolutionChooser.Dispose();
           
        }
    }
}
