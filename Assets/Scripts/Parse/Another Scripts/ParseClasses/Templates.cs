using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Templates : MonoBehaviour  {

	#region depricated
	[System.Serializable]
	public class User{
		public string name;
		public string password;
		public string userId;

		public User (string userId ,string name, string password){
			this.userId = userId;
			this.name = name;
			this.password = password;
		}

		public User(){}
	}
	#endregion

	[System.Serializable]
	public class Room{
		public List<string> clientsIds = new List<string> ();
		public int RoomNumber;

		public Room (User user, string clientID){
		}
	}
		
	[System.Serializable]
	public class Statistics{
		public int RoomNumber;
		public int ClientID;

		public Statistics(int roomNumber, int clientId){
				
		}
	}

	[System.Serializable]
	public class Mafia{
		public enum Characteristic{
			Killer,
			Stroger,
			Agility,
			Joker
		}

		public enum Research
		{
			Hide,
			Vision,
			Radiance,
			Spy,
			Butcher,
			Bounty,
			Thief
		}
		public Research research;
		public Characteristic character;
		public Dictionary<Research, int> AbilityDictionary = new Dictionary<Mafia.Research, int> ();
		public Dictionary<Characteristic, int> CharacteristicDictionary = new Dictionary<Characteristic, int> ();
		public int TotalCount;

		public Mafia(int sendCount, Dictionary<Characteristic,  int> sendCharacteristics, Dictionary<Research, int> sendAbility){
			this.TotalCount = sendCount;
			this.CharacteristicDictionary = sendCharacteristics;
			this.AbilityDictionary = sendAbility;
		}
	}

	public class SendData{
		public string senderId;
		public string targetId;
		public Mafia mafia;

		public SendData(string senderId, string targetId, Mafia mafia){
			this.senderId = senderId;
			this.targetId = targetId;
			this.mafia = mafia;
		}
	}

	public class Request{
		public enum Requests{
			getId,
			getRoom,
			logOutRoom,
			ready,
			go
		}

		public Request (Requests request){
		}
	}
	public enum CustomTables{
		waitRoom
	}
}
