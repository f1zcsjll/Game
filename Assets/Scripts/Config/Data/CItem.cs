using Base;

namespace Config.Data {
	public class CItem :BaseData {
		public enum Type{
			ID,
			Name,
			Type,
			AddHp,
			AddMp,
		};

		public override BaseData ReadData(string data) {
			string[] temp = data.Split(',');
			for (int i = 0; i < temp.Length;i++) {
				DataList.Add((Type)i, temp[i]); }
			return this;
}}}
