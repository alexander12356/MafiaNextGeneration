using System.Collections;
using System.Collections.Generic;
using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration
{
    public struct PersonData
    {
        public List<BuffType> BuffList;
    }

    public struct PersonDataList
    {
        public PersonType Type;
        public List<PersonData> DataList;
    }

    public class WorldData
    {
        public float CreatePersonTime = 1;
        public float DeadPersonTime = 1;
        public float MutationFrequency;
        public List<PersonMutationChance> PersonMutationChance = new List<PersonMutationChance>();
        public List<PersonBuffChance> PersonBuffChance = new List<PersonBuffChance>();
        public float CitizenDeathrate;
        public float MafiaArrestChance;
        public float PolicemanKilledChance;
        public float DiminishingMafia;
        public float Invisibility;
        public int PersonCount = 0;
        public int CitizenCount = 0;
        public int MafiaCount = 0;
        public int PolicemanCount = 0;
        public int MafiaKillerCount = 0;
        public List<BuffData> BuffList = new List<BuffData>();

        public List<PersonDataList> PersonDataList = new List<PersonDataList>();
    }
}