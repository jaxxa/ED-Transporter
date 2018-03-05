using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.Transporter
{
    class GameComponent_Transporter : GameComponent
    {

        public static GameComponent_Transporter TransportControler()
        {
            return GameComponent_Transporter.m_TransporterControler;
        }
        private static GameComponent_Transporter m_TransporterControler;


        private List<Thing> StoredThings
        {
            get
            {
                if (this.m_StoredThings == null)
                {
                    this.m_StoredThings = new List<Thing>();
                }
                return this.m_StoredThings;
            }
        }
        private List<Thing> m_StoredThings;

        Game game;

        public GameComponent_Transporter(Verse.Game game)
        {
            this.game = game;
            GameComponent_Transporter.m_TransporterControler = this;
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

            Scribe_Collections.Look<Thing>(ref this.m_StoredThings, "m_StoredThings", LookMode.Deep);
        }

        public void StoreThing(List<Thing> thingsToStore)
        {
            this.StoredThings.AddRange(thingsToStore);
        }

        public void StoreThing(Thing thingToStore)
        {
            this.StoredThings.Add(thingToStore);

        }

        public List<Thing> RetreiveThings()
        {
            List<Thing> _ThingsToReturn = this.StoredThings.ToList();
            this.StoredThings.Clear();
            return _ThingsToReturn;
        }

    }
}
