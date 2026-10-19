using UnityEngine;

public interface IDataPersistence
{
   void LoadData(GameData data);
   void SaveData(ref GameData data);

   void LoadPrevBattleData(DynamicGameData data);
   void SavePrevBattleData(ref DynamicGameData data);
}
