using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using UnityEngine;
using RimWorld;

namespace EnhancedDevelopment.Transporter
{
    [StaticConstructorOnStartup]
    class Building_Transporter : Verse.Building
    {

        static Building_Transporter()
        {

            //Verse.GameComponentUtility.

        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            //this.m_TransporterComp = (GameComponent_Transporter)Current.Game.GetComponent(typeof(GameComponent_Transporter));

        }

        public override void Tick()
        {
            base.Tick();
            //GameComponent_Transporter.TransporterComponent.TestMessage();
            //this.m_TransporterComp.TestMessage();
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            //return base.GetGizmos();

            //Add the stock Gizmoes
            foreach (var g in base.GetGizmos())
            {
                yield return g;
            }

            if (true)
            {
                Command_Action act = new Command_Action();
                //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                act.action = () => this.TransporterBeamResourcesOut();
                //    act.icon = UI_ADD_RESOURCES;
                act.defaultLabel = "Beam Resources Out";
                act.defaultDesc = "Beam Resources Out";
                act.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act;
            }

            if (true)
            {
                Command_Action act = new Command_Action();
                //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                act.action = () => this.TransporterBeamCrewOut();
                //  act.icon = UI_ADD_COLONIST;
                act.defaultLabel = "Beam Crew Out";
                act.defaultDesc = "Beam Crew Out";
                act.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act;
            }

            if (true)
            {
                Command_Action act = new Command_Action();
                //act.action = () => Designator_Deconstruct.DesignateDeconstruct(this);
                act.action = () => this.TransporterBeamIn();
                //act.icon = UI_GATE_IN;
                act.defaultLabel = "Beam In";
                act.defaultDesc = "Beam In";
                act.activateSound = SoundDef.Named("Click");
                //act.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
                //act.groupKey = 689736;
                yield return act;
            }


        }

        private void TransporterBeamResourcesOut()
        {

            List<Thing> foundThings = GenRadial.RadialDistinctThingsAround(this.Position, this.Map, 5, true).Where(x => x.def.category == ThingCategory.Item).Where(x => x.Spawned).ToList();

            if (foundThings.Any())
            {
                Log.Message("Found:" + foundThings.Count().ToString());

                GameComponent_Transporter.TransportControler().StoreThing(foundThings);
                foundThings.ForEach(x =>
                {
                    Log.Message("Removing: " + x.def.defName);
                    x.DeSpawn();

                    // Tell the MapDrawer that here is something thats changed
                    this.Map.mapDrawer.MapMeshDirty(x.Position, MapMeshFlag.Things, true, false);
                });


            }
        }

        private void TransporterBeamCrewOut()
        {
            List<Pawn> _ClosePawns = this.Map.mapPawns.PawnsInFaction(Faction.OfPlayer).Where(P => P.Position.InHorDistOf(this.Position, 5)).ToList();

            _ClosePawns.ToList().ForEach(_CurrentPawn =>
            {
                if (_CurrentPawn.Spawned)
                {

                    GameComponent_Transporter.TransportControler().StoreThing(_CurrentPawn);

                    _CurrentPawn.DeSpawn();

                    // Tell the MapDrawer that here is something thats changed
                    this.Map.mapDrawer.MapMeshDirty(_CurrentPawn.Position, MapMeshFlag.Things, true, false);
                }
            });
        }

        private void TransporterBeamIn()
        {
            List<Thing> _NewThings = GameComponent_Transporter.TransportControler().RetreiveThings();

            _NewThings.ForEach(_Thing =>
            {

                Log.Message("Placing:" + _Thing.def.defName);

                if (_Thing.def.CanHaveFaction)
                {
                    _Thing.SetFactionDirect(RimWorld.Faction.OfPlayer);
                }

                GenPlace.TryPlaceThing(_Thing, this.Position, this.Map, ThingPlaceMode.Near);
                //GenSpawn.Spawn(x,this.Position, this.Map);

                Pawn _Pawn = _Thing as Pawn;
                if (_Pawn != null)
                {
                    if (_Pawn.RaceProps.Humanlike)
                    {
                        TaleRecorder.RecordTale(TaleDefOf.LandedInPod, _Pawn);
                        //TaleRecorder.RecordTale(TaleDefOf.LandedInPod, pawn);
                    }

                    if (_Pawn.IsColonist && _Pawn.Spawned && !base.Map.IsPlayerHome)
                    {
                        _Pawn.drafter.Drafted = true;
                    }

                }

            });

        }
    }

}
