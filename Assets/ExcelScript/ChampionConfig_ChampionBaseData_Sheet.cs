﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EasyExcel.
//     Runtime Version: 4.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;
using EasyExcel;

namespace ExcelConfig
{
	[Serializable]
	public class ChampionBaseData : EERowData
	{
		[EEKeyField]
		[SerializeField]
		private int _ID;
		public int ID { get { return _ID; } }

		[SerializeField]
		private string _name;
		public string name { get { return _name; } }

		[SerializeField]
		private string _prefab;
		public string prefab { get { return _prefab; } }

		[SerializeField]
		private string _attackProjectile;
		public string attackProjectile { get { return _attackProjectile; } }

		[SerializeField]
		private int _cost;
		public int cost { get { return _cost; } }

		[SerializeField]
		private string _attackType;
		public string attackType { get { return _attackType; } }

		[SerializeField]
		private string _type1;
		public string type1 { get { return _type1; } }

		[SerializeField]
		private string _type2;
		public string type2 { get { return _type2; } }

		[SerializeField]
		private string _type3;
		public string type3 { get { return _type3; } }

		[SerializeField]
		private string[] _otherTypes;
		public string[] otherTypes { get { return _otherTypes; } }


		public ChampionBaseData()
		{
		}

#if UNITY_EDITOR
		public ChampionBaseData(List<List<string>> sheet, int row, int column)
		{
			TryParse(sheet[row][column++], out _ID);
			TryParse(sheet[row][column++], out _name);
			TryParse(sheet[row][column++], out _prefab);
			TryParse(sheet[row][column++], out _attackProjectile);
			TryParse(sheet[row][column++], out _cost);
			TryParse(sheet[row][column++], out _attackType);
			TryParse(sheet[row][column++], out _type1);
			TryParse(sheet[row][column++], out _type2);
			TryParse(sheet[row][column++], out _type3);
			string[] _otherTypesArray = sheet[row][column++].Split(',');
			int _otherTypesCount = _otherTypesArray.Length;
			_otherTypes = new string[_otherTypesCount];
			for(int i = 0; i < _otherTypesCount; i++)
				TryParse(_otherTypesArray[i], out _otherTypes[i]);
		}
#endif
		public override void OnAfterSerialized()
		{
		}
	}

	public class ChampionConfig_ChampionBaseData_Sheet : EERowDataCollection
	{
		[SerializeField]
		private List<ChampionBaseData> elements = new List<ChampionBaseData>();

		public override void AddData(EERowData data)
		{
			elements.Add(data as ChampionBaseData);
		}

		public override int GetDataCount()
		{
			return elements.Count;
		}

		public override EERowData GetData(int index)
		{
			return elements[index];
		}

		public override void OnAfterSerialized()
		{
			foreach (var element in elements)
				element.OnAfterSerialized();
		}
	}
}
