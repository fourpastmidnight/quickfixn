﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QuickFix;
using QuickFix.Fields;

namespace Executor
{
    public class Executor : QuickFix.MessageCracker, QuickFix.Application
    {
        int orderID = 0;
        int execID = 0;

        private string GenOrderID() { return (++orderID).ToString(); }
        private string GenExecID() { return (++execID).ToString(); }

        #region QuickFix.Application Methods

        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("IN:  " + message);
            Crack(message, sessionID);
        }

        public void ToApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("OUT: " + message);
        }

        public void FromAdmin(Message message, SessionID sessionID) { }
        public void OnCreate(SessionID sessionID) { }
        public void OnLogout(SessionID sessionID) { }
        public void OnLogon(SessionID sessionID) { }
        public void ToAdmin(Message message, SessionID sessionID) { }
        #endregion

        #region MessageCracker overloads
        public void OnMessage(QuickFix.FIX40.NewOrderSingle n, SessionID s)
        {
            Symbol symbol = n.symbol;
            Side side = n.side;
            OrdType ordType = n.ordType;
            OrderQty orderQty = n.orderQty;
            Price price = n.price;
            ClOrdID clOrdID = n.clOrdID;

            if (ordType.getValue() != OrdType.LIMIT)
                throw new IncorrectTagValue(ordType.Tag);

            QuickFix.FIX40.ExecutionReport exReport = new QuickFix.FIX40.ExecutionReport(
                new OrderID(GenOrderID()),
                new ExecID(GenExecID()),
                new ExecTransType(ExecTransType.NEW),
                new OrdStatus(OrdStatus.FILLED),
                symbol,
                side,
                orderQty,
                new LastShares(orderQty.getValue()),
                new LastPx(price.getValue()),
                new CumQty(orderQty.getValue()),
                new AvgPx(price.getValue()));

            exReport.set(clOrdID);

            if (n.isSetAccount())
                exReport.setField(n.account);

            try
            {
                Session.SendToTarget(exReport, s);
            }
            catch (SessionNotFound ex)
            {
                Console.WriteLine("==session not found exception!==");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("==unknown exception==");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void OnMessage(QuickFix.FIX41.NewOrderSingle n, SessionID s)
        {
            Symbol symbol = n.symbol;
            Side side = n.side;
            OrdType ordType = n.ordType;
            OrderQty orderQty = n.orderQty;
            Price price = n.price;
            ClOrdID clOrdID = n.clOrdID;

            if (ordType.getValue() != OrdType.LIMIT)
                throw new IncorrectTagValue(ordType.Tag);

            QuickFix.FIX41.ExecutionReport exReport = new QuickFix.FIX41.ExecutionReport(
                new OrderID(GenOrderID()),
                new ExecID(GenExecID()),
                new ExecTransType(ExecTransType.NEW),
                new ExecType(ExecType.FILL),
                new OrdStatus(OrdStatus.FILLED),
                symbol,
                side,
                orderQty,
                new LastShares(orderQty.getValue()),
                new LastPx(price.getValue()),
                new LeavesQty(0),
                new CumQty(orderQty.getValue()),
                new AvgPx(price.getValue()));

            exReport.set(clOrdID);

            if (n.isSetAccount())
                exReport.setField(n.account);

            try
            {
                Session.SendToTarget(exReport, s);
            }
            catch (SessionNotFound ex)
            {
                Console.WriteLine("==session not found exception!==");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("==unknown exception==");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void OnMessage(QuickFix.FIX42.NewOrderSingle n, SessionID s)
        {
            Symbol symbol = n.symbol;
            Side side = n.side;
            OrdType ordType = n.ordType;
            OrderQty orderQty = n.orderQty;
            Price price = n.price;
            ClOrdID clOrdID = n.clOrdID;

            if (ordType.getValue() != OrdType.LIMIT)
                throw new IncorrectTagValue(ordType.Tag);

            QuickFix.FIX42.ExecutionReport exReport = new QuickFix.FIX42.ExecutionReport(
                new OrderID(GenOrderID()),
                new ExecID(GenExecID()),
                new ExecTransType(ExecTransType.NEW),
                new ExecType(ExecType.FILL),
                new OrdStatus(OrdStatus.FILLED),
                symbol,
                side,
                new LeavesQty(0),
                new CumQty(orderQty.getValue()),
                new AvgPx(price.getValue()));

            exReport.set(clOrdID);
            exReport.set(orderQty);
            exReport.set(new LastShares(orderQty.getValue()));
            exReport.set(new LastPx(price.getValue()));

            if (n.isSetAccount())
                exReport.setField(n.account);

            try
            {
                Session.SendToTarget(exReport, s);
            }
            catch (SessionNotFound ex)
            {
                Console.WriteLine("==session not found exception!==");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("==unknown exception==");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void OnMessage(QuickFix.FIX43.NewOrderSingle n, SessionID s)
        {
            Symbol symbol = n.symbol;
            Side side = n.side;
            OrdType ordType = n.ordType;
            OrderQty orderQty = n.orderQty;
            Price price = n.price;
            ClOrdID clOrdID = n.clOrdID;

            if (ordType.getValue() != OrdType.LIMIT)
                throw new IncorrectTagValue(ordType.Tag);

            QuickFix.FIX43.ExecutionReport exReport = new QuickFix.FIX43.ExecutionReport(
                new OrderID(GenOrderID()),
                new ExecID(GenExecID()),
                new ExecType(ExecType.FILL),
                new OrdStatus(OrdStatus.FILLED),
                symbol, // Shouldn't be here?
                side,
                new LeavesQty(0),
                new CumQty(orderQty.getValue()),
                new AvgPx(price.getValue()));

            exReport.set(clOrdID);
            exReport.set(symbol);
            exReport.set(orderQty);
            exReport.set(new LastQty(orderQty.getValue()));
            exReport.set(new LastPx(price.getValue()));

            if (n.isSetAccount())
                exReport.setField(n.account);

            try
            {
                Session.SendToTarget(exReport, s);
            }
            catch (SessionNotFound ex)
            {
                Console.WriteLine("==session not found exception!==");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("==unknown exception==");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void OnMessage(QuickFix.FIX44.NewOrderSingle n, SessionID s)
        {
            Symbol symbol = n.symbol;
            Side side = n.side;
            OrdType ordType = n.ordType;
            OrderQty orderQty = n.orderQty;
            Price price = n.price;
            ClOrdID clOrdID = n.clOrdID;

            if (ordType.getValue() != OrdType.LIMIT)
                throw new IncorrectTagValue(ordType.Tag);

            QuickFix.FIX44.ExecutionReport exReport = new QuickFix.FIX44.ExecutionReport(
                new OrderID(GenOrderID()),
                new ExecID(GenExecID()),
                new ExecType(ExecType.FILL),
                new OrdStatus(OrdStatus.FILLED),
                symbol, //shouldn't be here?
                side,
                new LeavesQty(0),
                new CumQty(orderQty.getValue()),
                new AvgPx(price.getValue()));

            exReport.set(clOrdID);
            exReport.set(symbol);
            exReport.set(orderQty);
            exReport.set(new LastQty(orderQty.getValue()));
            exReport.set(new LastPx(price.getValue()));

            if (n.isSetAccount())
                exReport.setField(n.account);

            try
            {
                Session.SendToTarget(exReport, s);
            }
            catch (SessionNotFound ex)
            {
                Console.WriteLine("==session not found exception!==");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("==unknown exception==");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void OnMessage(QuickFix.FIX50.NewOrderSingle n, SessionID s)
        {
            Symbol symbol = n.symbol;
            Side side = n.side;
            OrdType ordType = n.ordType;
            OrderQty orderQty = n.orderQty;
            Price price = n.price;
            ClOrdID clOrdID = n.clOrdID;

            if (ordType.getValue() != OrdType.LIMIT)
                throw new IncorrectTagValue(ordType.Tag);

            QuickFix.FIX50.ExecutionReport exReport = new QuickFix.FIX50.ExecutionReport(
                new OrderID(GenOrderID()),
                new ExecID(GenExecID()),
                new ExecType(ExecType.FILL),
                new OrdStatus(OrdStatus.FILLED),
                side,
                new LeavesQty(0),
                new CumQty(orderQty.getValue()));

            exReport.set(clOrdID);
            exReport.set(symbol);
            exReport.set(orderQty);
            exReport.set(new LastQty(orderQty.getValue()));
            exReport.set(new LastPx(price.getValue()));
            exReport.set(new AvgPx(price.getValue()));

            if (n.isSetAccount())
                exReport.setField(n.account);

            try
            {
                Session.SendToTarget(exReport, s);
            }
            catch (SessionNotFound ex)
            {
                Console.WriteLine("==session not found exception!==");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("==unknown exception==");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion
    }
}