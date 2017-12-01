using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Transporter
{
    class GameComponent_Transporter : GameComponent
    {

        private static GameComponent_Transporter TransporterControler;
        public static GameComponent_Transporter GetTransportControler()
        {
            return GameComponent_Transporter.TransporterControler;
        }



        private List<Thing> m_StoredThings = new List<Thing>();

        Game game;

        public GameComponent_Transporter(Verse.Game game)
        {
            this.game = game;
            GameComponent_Transporter.TransporterControler = this;
        }


        public override void GameComponentTick()
        {
            base.GameComponentTick();
            //Log.Message("Test Tick");
        }

        public void TestMessage()
        {
            Log.Message("Ping");
        }

        public override void ExposeData()
        {
            base.ExposeData();
        }
        
        public void StoreThing(List<Thing> thingsToStore)
        {
            this.m_StoredThings.AddRange(thingsToStore);
        }

        public void StoreThing(Thing thingToStore)
        {
            this.m_StoredThings.Add(thingToStore);

        }

        public List<Thing> RetreiveThings()
        {
            List<Thing> _ThingsToReturn = this.m_StoredThings.ToList();
            this.m_StoredThings.Clear();
            return _ThingsToReturn;
        }

    }
}
