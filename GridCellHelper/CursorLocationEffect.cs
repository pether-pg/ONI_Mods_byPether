using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klei.AI;

namespace GridCellHelper
{
    class CursorLocationEffect : KMonoBehaviour, ISim4000ms
    {
        const string EFFECT_ID = "CursorLocationEffectId";

        public void Sim4000ms(float dt)
        {
            int cell = Grid.XYToCell(106, 347);
            CameraController.Instance.ActiveWorldStarWipe(this.gameObject.GetMyWorldId(), Grid.CellToPos(cell), 8f);
        }

        string GetText()
        {
            int cell = Grid.PosToCell(KInputManager.GetMousePos());
            if (!Grid.IsValidCell(cell))
                return "Invalid cell";

            int x, y;
            Grid.CellToXY(cell, out x, out y);

            string str = $"Current cell = {cell}; x = {x}; y = {y}";

            return str;
        }
    }
}
