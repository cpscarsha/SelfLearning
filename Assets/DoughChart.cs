using UnityEngine;
using UnityEngine.UI;
using XCharts.Runtime;
#if INPUT_SYSTEM_ENABLED
using Input = XCharts.Runtime.InputHelper;
#endif



namespace XCharts.Example
{
    [DisallowMultipleComponent]
    //[ExecuteInEditMode]
    public class DoughChart : MonoBehaviour
    {
        
        BaseChart chart;
        void Awake()
        {
            //AddChart();
        }


        void Update()
        {
            if (Scene.ChartTrg1)
            {
                AddChart();
                Scene.ChartTrg1=false;
            }
        }

        void AddChart()
        {
            chart = gameObject.GetComponent<BaseChart>();
            if (chart == null)
            {
                chart = gameObject.AddComponent<LineChart>();
                chart.Init();
                chart.SetSize(1200, 600);
            }
            var title = chart.EnsureChartComponent<Title>();
            title.text = "Poison Resistant";
            title.subText = "";

            var tooltip = chart.EnsureChartComponent<Tooltip>();
            tooltip.show = true;

            var legend = chart.EnsureChartComponent<Legend>();
            legend.show = false;

            var xAxis = chart.EnsureChartComponent<XAxis>();
            xAxis.splitNumber = 10;
            xAxis.boundaryGap = true;
            xAxis.type = Axis.AxisType.Category;

            var yAxis = chart.EnsureChartComponent<YAxis>();
            yAxis.type = Axis.AxisType.Value;

            
            chart.AddSerie<Line>("line");

            chart.AddXAxisData(Scene.Round.ToString());
            chart.AddData(0,Scene.ToxicGuysCount);
        }

        void ModifyComponent()
        {

        }
    }
}