using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient.Model
{
    public class PreFlopStats
    {
        private float pfr;

        public float Pfr
        {
            get { return pfr; }
            set { pfr = value; }
        }
        private float vpip;

        public float Vpip
        {
            get { return vpip; }
            set { vpip = value; }
        }
        private float _3bet;

        public float _3bet1
        {
            get { return _3bet; }
            set { _3bet = value; }
        }
        private float _4bet;

        public float _4bet1
        {
            get { return _4bet; }
            set { _4bet = value; }
        }
        private float _5bet;

        public float _5bet1
        {
            get { return _5bet; }
            set { _5bet = value; }
        }
        private float fTo3Bet;

        public float FTo3Bet
        {
            get { return fTo3Bet; }
            set { fTo3Bet = value; }
        }
        private float fTo4Bet;

        public float FTo4Bet
        {
            get { return fTo4Bet; }
            set { fTo4Bet = value; }
        }
        private float fTo5Bet;

        public float FTo5Bet
        {
            get { return fTo5Bet; }
            set { fTo5Bet = value; }
        }
        private float coSteal;

        public float CoSteal
        {
            get { return coSteal; }
            set { coSteal = value; }
        }
        private float btnSteal;

        public float BtnSteal
        {
            get { return btnSteal; }
            set { btnSteal = value; }
        }
        private float sbSteal;

        public float SbSteal
        {
            get { return sbSteal; }
            set { sbSteal = value; }
        }
        private float reSteal;

        public float ReSteal
        {
            get { return reSteal; }
            set { reSteal = value; }
        }
        private float reReSteal;

        public float ReReSteal
        {
            get { return reReSteal; }
            set { reReSteal = value; }
        }
        private float foldToSteal;

        public float FoldToSteal
        {
            get { return foldToSteal; }
            set { foldToSteal = value; }
        }
        private float foldToResteal;

        public float FoldToResteal
        {
            get { return foldToResteal; }
            set { foldToResteal = value; }
        }
        private float bbFoldVsSbSteal;

        public float BbFoldVsSbSteal
        {
            get { return bbFoldVsSbSteal; }
            set { bbFoldVsSbSteal = value; }
        }
        private float bbRestealVsSb;

        public float BbRestealVsSb
        {
            get { return bbRestealVsSb; }
            set { bbRestealVsSb = value; }
        }
    }
}
