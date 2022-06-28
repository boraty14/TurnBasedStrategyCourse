using System.Collections.Generic;

namespace Grid
{
    public class GridObject
    {
        private GridPosition _gridPosition;
        private GridSystem _gridSystem;
        private List<Unit> _unitList;

        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            _gridSystem = gridSystem;
            _gridPosition = gridPosition;
            _unitList = new List<Unit>();
        }

        public void AddUnit(Unit unit) => _unitList.Add(unit);
        public List<Unit> GetUnitList() => _unitList;
        public void RemoveUnit(Unit unit) => _unitList.Remove(unit);

        public override string ToString()
        {
            string unitString = "";
            foreach (var unit in _unitList)
            {
                unitString += unit + "\n";
            }
            return _gridPosition.ToString() + "\n" + unitString;
        }
    }
}
