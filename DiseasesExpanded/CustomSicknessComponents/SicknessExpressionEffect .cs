using Klei.AI;
using UnityEngine;

namespace DiseasesExpanded
{
    class SicknessExpressionEffect : Sickness.SicknessComponent
	{
		private readonly Expression expression;

		public SicknessExpressionEffect(Expression expression)
		{
			this.expression = expression;
		}

		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			go.GetComponent<FaceGraph>().AddExpression(expression);
			return null;
		}

		public override void OnCure(GameObject go, object instace_data)
		{
			go.GetComponent<FaceGraph>().RemoveExpression(expression);
		}
	}
}
