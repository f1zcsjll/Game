using Base;

namespace Config.Data {
	public class CJob :BaseData {
		public enum Type{
			ID,
			Name,
			type,
			Hp,
			Mp,
			Power,
		};

		public override BaseData ReadData(string data) {
			string[] temp = data.Split(',');
			for (int i = 0; i < temp.Length;i++) {
				DataList.Add((Type)i, temp[i]); }
			return this;
}}}
