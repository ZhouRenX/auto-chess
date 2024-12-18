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
	public class ConstructorBonusType : EERowData
	{
		[EEKeyField]
		[SerializeField]
		private string _name;
		public string name { get { return _name; } }

		[SerializeField]
		private string _c_name;
		public string c_name { get { return _c_name; } }

		[SerializeField]
		private string _icon;
		public string icon { get { return _icon; } }

		[Serializable]
		public class BonusClass
		{
			public int count;
			public int buff_ID;
		}
		[SerializeField]
		private BonusClass[] _Bonus;
		public BonusClass[] Bonus { get { return _Bonus; } }

		[SerializeField]
		private string _description;
		public string description { get { return _description; } }


		public ConstructorBonusType()
		{
		}

#if UNITY_EDITOR
		public ConstructorBonusType(List<List<string>> sheet, int row, int column)
		{
			TryParse(sheet[row][column++], out _name);
			TryParse(sheet[row][column++], out _c_name);
			TryParse(sheet[row][column++], out _icon);
			string rawBonus = sheet[row][column++];
			string[] subsBonus_0 = rawBonus.Split(';');
			_Bonus = new BonusClass[subsBonus_0.Length];
			for (int j = 0; j < subsBonus_0.Length; ++j)
			{
				var _Bonusone = new BonusClass();
				_Bonus[j] = _Bonusone;
				string[] subsBonus_1 = subsBonus_0[j].Split(',');
				for (int i = 0; i < subsBonus_1.Length; ++i)
				{
					var strValue = subsBonus_1[i];
					if (i == 0)
						TryParse(strValue, out _Bonusone.count);
					else if (i == 1)
						TryParse(strValue, out _Bonusone.buff_ID);
				}
			}
			TryParse(sheet[row][column++], out _description);
		}
#endif
		public override void OnAfterSerialized()
		{
		}
	}

	public class ConstructorConfig_ConstructorBonusType_Sheet : EERowDataCollection
	{
		[SerializeField]
		private List<ConstructorBonusType> elements = new List<ConstructorBonusType>();

		public override void AddData(EERowData data)
		{
			elements.Add(data as ConstructorBonusType);
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
