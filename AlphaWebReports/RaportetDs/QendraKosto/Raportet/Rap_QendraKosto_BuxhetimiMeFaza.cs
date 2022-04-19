using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;


namespace AlphaWebReports.RaportetDs.QendraKosto.Raportet
{
    public partial class Rap_QendraKosto_BuxhetimiMeFaza : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_QendraKosto_BuxhetimiMeFaza(){InitializeComponent();} 

        private int llojFaze;
       

        private void Rap_Analiza_QendraKosto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
        public Rap_QendraKosto_BuxhetimiMeFaza(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }

        public Rap_QendraKosto_BuxhetimiMeFaza(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
           
            xrLabel57.Text = raport.Parameters["filterQK"].Description;
            parameter1.Value = raport.Parameters["filterQK"].Value;


            xrLabel46.Text = raport.Parameters["filterDtDokQenderKosto"].Description;
            parameter3.Value = raport.Parameters["filterDtDokQenderKosto"].Value;
            xrLabel60.Text = raport.Parameters["llojFaze"].Description;
            int.TryParse(raport.Parameters["llojFaze"].Value.ToString(), out llojFaze);
            parameter4.Value = emerFaze();

            EmrateLabelave(ci);
            
                
            

            percaktoLabelTePadukshem();
           
        }

        private void percaktoLabelTePadukshem()
        {
            if (llojFaze == 0)
                return;
            if(llojFaze >= 1)
            {
                setFaza12NotVisible();
                setFaza11NotVisible();
                setFaza10NotVisible();
                setFaza9NotVisible();
                setFaza8NotVisible();
                setFaza7NotVisible();
                setFaza6NotVisible();
                setFaza5NotVisible();
            }
            if(llojFaze >= 2)
            {
                setFaza4NotVisible();
            }
            if(llojFaze >= 3)
            {
                setFaza3NotVisible();
                
            }
            if(llojFaze >= 4)
            {
                setFaza2NotVisible();
                setFaza1NotVisible();
            }

           
        }
        #region vendosja e labelave te fazave Visible = false
        private void setFaza12NotVisible()
        {
            xrLabel488.Visible = false;
            xrLabel481.Visible = false;
            xrLabel125.Visible = false;
            xrLabel489.Visible = false;
            xrLabel490.Visible = false;
            xrLabel491.Visible = false;

            xrLabel499.Visible = false;
            xrLabel500.Visible = false;
            xrLabel501.Visible = false;
            xrLabel502.Visible = false;
            xrLabel503.Visible = false;

            xrLabel211.Visible = false;
            xrLabel212.Visible = false;
            xrLabel213.Visible = false;
            xrLabel214.Visible = false;
            xrLabel215.Visible = false;

            xrLabel290.Visible = false;
            xrLabel291.Visible = false;
            xrLabel292.Visible = false;
            xrLabel293.Visible = false;
            xrLabel294.Visible = false;

            xrLabel354.Visible = false;
            xrLabel355.Visible = false;
            xrLabel356.Visible = false;
            xrLabel357.Visible = false;
            xrLabel358.Visible = false;

            xrLabel419.Visible = false;
            xrLabel420.Visible = false;
            xrLabel421.Visible = false;
            xrLabel422.Visible = false;
            xrLabel423.Visible = false;

            xrLabel511.Visible = false;
            xrLabel512.Visible = false;
            xrLabel513.Visible = false;
            xrLabel514.Visible = false;
            xrLabel515.Visible = false;

            xrLabel571.Visible = false;
            xrLabel572.Visible = false;
            xrLabel573.Visible = false;
            xrLabel574.Visible = false;
            xrLabel575.Visible = false;
        }

        private void setFaza11NotVisible()
        {
            xrLabel484.Visible = false;
            xrLabel486.Visible = false;
            xrLabel482.Visible = false;
            xrLabel114.Visible = false;
            xrLabel118.Visible = false;
            xrLabel119.Visible = false;

            xrLabel169.Visible = false;
            xrLabel495.Visible = false;
            xrLabel496.Visible = false;
            xrLabel497.Visible = false;
            xrLabel498.Visible = false;

            xrLabel206.Visible = false;
            xrLabel207.Visible = false;
            xrLabel208.Visible = false;
            xrLabel209.Visible = false;
            xrLabel210.Visible = false;

            xrLabel285.Visible = false;
            xrLabel286.Visible = false;
            xrLabel287.Visible = false;
            xrLabel288.Visible = false;
            xrLabel289.Visible = false;

            xrLabel349.Visible = false;
            xrLabel350.Visible = false;
            xrLabel351.Visible = false;
            xrLabel352.Visible = false;
            xrLabel353.Visible = false;

            xrLabel414.Visible = false;
            xrLabel415.Visible = false;
            xrLabel416.Visible = false;
            xrLabel417.Visible = false;
            xrLabel418.Visible = false;

            xrLabel506.Visible = false;
            xrLabel507.Visible = false;
            xrLabel508.Visible = false;
            xrLabel509.Visible = false;
            xrLabel510.Visible = false;

            xrLabel566.Visible = false;
            xrLabel567.Visible = false;
            xrLabel568.Visible = false;
            xrLabel569.Visible = false;
            xrLabel570.Visible = false;
        }

        private void setFaza10NotVisible()
        {
            xrLabel120.Visible = false;
            xrLabel124.Visible = false;
            xrLabel487.Visible = false;
            xrLabel121.Visible = false;
            xrLabel480.Visible = false;
            xrLabel483.Visible = false;

            xrLabel164.Visible = false;
            xrLabel165.Visible = false;
            xrLabel166.Visible = false;
            xrLabel167.Visible = false;
            xrLabel168.Visible = false;

            xrLabel205.Visible = false;
            xrLabel204.Visible = false;
            xrLabel203.Visible = false;
            xrLabel202.Visible = false;
            xrLabel201.Visible = false;

            xrLabel280.Visible = false;
            xrLabel281.Visible = false;
            xrLabel282.Visible = false;
            xrLabel283.Visible = false;
            xrLabel284.Visible = false;

            xrLabel344.Visible = false;
            xrLabel345.Visible = false;
            xrLabel346.Visible = false;
            xrLabel347.Visible = false;
            xrLabel348.Visible = false;

            xrLabel409.Visible = false;
            xrLabel410.Visible = false;
            xrLabel411.Visible = false;
            xrLabel412.Visible = false;
            xrLabel413.Visible = false;

            xrLabel473.Visible = false;
            xrLabel474.Visible = false;
            xrLabel475.Visible = false;
            xrLabel479.Visible = false;
            xrLabel505.Visible = false;

            xrLabel561.Visible = false;
            xrLabel562.Visible = false;
            xrLabel563.Visible = false;
            xrLabel564.Visible = false;
            xrLabel565.Visible = false;
        }

        private void setFaza9NotVisible()
        {
            xrLabel123.Visible = false;
            xrLabel116.Visible = false;
            xrLabel117.Visible = false;
            xrLabel485.Visible = false;
            xrLabel122.Visible = false;
            xrLabel115.Visible = false;

            xrLabel159.Visible = false;
            xrLabel160.Visible = false;
            xrLabel161.Visible = false;
            xrLabel162.Visible = false;
            xrLabel163.Visible = false;

            xrLabel220.Visible = false;
            xrLabel219.Visible = false;
            xrLabel218.Visible = false;
            xrLabel217.Visible = false;
            xrLabel216.Visible = false;

            xrLabel275.Visible = false;
            xrLabel276.Visible = false;
            xrLabel277.Visible = false;
            xrLabel278.Visible = false;
            xrLabel279.Visible = false;

            xrLabel339.Visible = false;
            xrLabel340.Visible = false;
            xrLabel341.Visible = false;
            xrLabel342.Visible = false;
            xrLabel343.Visible = false;

            xrLabel404.Visible = false;
            xrLabel405.Visible = false;
            xrLabel406.Visible = false;
            xrLabel407.Visible = false;
            xrLabel408.Visible = false;

            xrLabel468.Visible = false;
            xrLabel469.Visible = false;
            xrLabel470.Visible = false;
            xrLabel471.Visible = false;
            xrLabel472.Visible = false;

            xrLabel556.Visible = false;
            xrLabel557.Visible = false;
            xrLabel558.Visible = false;
            xrLabel559.Visible = false;
            xrLabel560.Visible = false;
        }

        private void setFaza8NotVisible()
        {
            xrLabel110.Visible = false;
            xrLabel103.Visible = false;
            xrLabel101.Visible = false;
            xrLabel111.Visible = false;
            xrLabel112.Visible = false;
            xrLabel113.Visible = false;

            xrLabel154.Visible = false;
            xrLabel155.Visible = false;
            xrLabel156.Visible = false;
            xrLabel157.Visible = false;
            xrLabel158.Visible = false;

            xrLabel225.Visible = false;
            xrLabel224.Visible = false;
            xrLabel223.Visible = false;
            xrLabel222.Visible = false;
            xrLabel221.Visible = false;

            xrLabel266.Visible = false;
            xrLabel267.Visible = false;
            xrLabel272.Visible = false;
            xrLabel273.Visible = false;
            xrLabel274.Visible = false;

            xrLabel334.Visible = false;
            xrLabel335.Visible = false;
            xrLabel336.Visible = false;
            xrLabel338.Visible = false;
            xrLabel337.Visible = false;

            xrLabel403.Visible = false;
            xrLabel402.Visible = false;
            xrLabel401.Visible = false;
            xrLabel400.Visible = false;
            xrLabel399.Visible = false;

            xrLabel463.Visible = false;
            xrLabel464.Visible = false;
            xrLabel465.Visible = false;
            xrLabel466.Visible = false;
            xrLabel467.Visible = false;

            xrLabel551.Visible = false;
            xrLabel552.Visible = false;
            xrLabel553.Visible = false;
            xrLabel554.Visible = false;
            xrLabel555.Visible = false;
        }

        private void setFaza7NotVisible()
        {
            xrLabel82.Visible = false;
            xrLabel106.Visible = false;
            xrLabel108.Visible = false;
            xrLabel104.Visible = false;
            xrLabel94.Visible = false;
            xrLabel95.Visible = false;

            xrLabel149.Visible = false;
            xrLabel150.Visible = false;
            xrLabel151.Visible = false;
            xrLabel152.Visible = false;
            xrLabel153.Visible = false;

            xrLabel230.Visible = false;
            xrLabel229.Visible = false;
            xrLabel228.Visible = false;
            xrLabel227.Visible = false;
            xrLabel226.Visible = false;

            xrLabel261.Visible = false;
            xrLabel262.Visible = false;
            xrLabel263.Visible = false;
            xrLabel264.Visible = false;
            xrLabel265.Visible = false;

            xrLabel329.Visible = false;
            xrLabel330.Visible = false;
            xrLabel331.Visible = false;
            xrLabel332.Visible = false;
            xrLabel333.Visible = false;

            xrLabel394.Visible = false;
            xrLabel395.Visible = false;
            xrLabel396.Visible = false;
            xrLabel397.Visible = false;
            xrLabel398.Visible = false;

            xrLabel458.Visible = false;
            xrLabel459.Visible = false;
            xrLabel460.Visible = false;
            xrLabel461.Visible = false;
            xrLabel462.Visible = false;

            xrLabel546.Visible = false;
            xrLabel547.Visible = false;
            xrLabel548.Visible = false;
            xrLabel549.Visible = false;
            xrLabel550.Visible = false;
        }

        private void setFaza6NotVisible()
        {
            xrLabel96.Visible = false;
            xrLabel109.Visible = false;
            xrLabel100.Visible = false;
            xrLabel102.Visible = false;
            xrLabel97.Visible = false;
            xrLabel105.Visible = false;

            xrLabel144.Visible = false;
            xrLabel145.Visible = false;
            xrLabel146.Visible = false;
            xrLabel147.Visible = false;
            xrLabel148.Visible = false;

            xrLabel191.Visible = false;
            xrLabel192.Visible = false;
            xrLabel193.Visible = false;
            xrLabel194.Visible = false;
            xrLabel195.Visible = false;

            xrLabel256.Visible = false;
            xrLabel257.Visible = false;
            xrLabel258.Visible = false;
            xrLabel259.Visible = false;
            xrLabel260.Visible = false;

            xrLabel324.Visible = false;
            xrLabel325.Visible = false;
            xrLabel326.Visible = false;
            xrLabel327.Visible = false;
            xrLabel328.Visible = false;

            xrLabel389.Visible = false;
            xrLabel390.Visible = false;
            xrLabel391.Visible = false;
            xrLabel392.Visible = false;
            xrLabel393.Visible = false;

            xrLabel453.Visible = false;
            xrLabel454.Visible = false;
            xrLabel455.Visible = false;
            xrLabel456.Visible = false;
            xrLabel457.Visible = false;

            xrLabel541.Visible = false;
            xrLabel542.Visible = false;
            xrLabel543.Visible = false;
            xrLabel544.Visible = false;
            xrLabel545.Visible = false;
        }

        private void setFaza5NotVisible()
        {
            xrLabel91.Visible = false;
            xrLabel107.Visible = false;
            xrLabel92.Visible = false;
            xrLabel93.Visible = false;
            xrLabel98.Visible = false;
            xrLabel99.Visible = false;

            xrLabel138.Visible = false;
            xrLabel139.Visible = false;
            xrLabel140.Visible = false;
            xrLabel142.Visible = false;
            xrLabel143.Visible = false;

            xrLabel200.Visible = false;
            xrLabel199.Visible = false;
            xrLabel198.Visible = false;
            xrLabel197.Visible = false;
            xrLabel196.Visible = false;

            xrLabel251.Visible = false;
            xrLabel252.Visible = false;
            xrLabel253.Visible = false;
            xrLabel254.Visible = false;
            xrLabel255.Visible = false;

            xrLabel315.Visible = false;
            xrLabel316.Visible = false;
            xrLabel317.Visible = false;
            xrLabel318.Visible = false;
            xrLabel319.Visible = false;

            xrLabel384.Visible = false;
            xrLabel385.Visible = false;
            xrLabel386.Visible = false;
            xrLabel387.Visible = false;
            xrLabel388.Visible = false;

            xrLabel448.Visible = false;
            xrLabel449.Visible = false;
            xrLabel450.Visible = false;
            xrLabel451.Visible = false;
            xrLabel452.Visible = false;

            xrLabel536.Visible = false;
            xrLabel537.Visible = false;
            xrLabel538.Visible = false;
            xrLabel539.Visible = false;
            xrLabel540.Visible = false;
        }

        private void setFaza4NotVisible()
        {
            xrLabel85.Visible = false;
            xrLabel86.Visible = false;
            xrLabel87.Visible = false;
            xrLabel88.Visible = false;
            xrLabel89.Visible = false;
            xrLabel90.Visible = false;

            xrLabel136.Visible = false;
            xrLabel137.Visible = false;
            xrLabel141.Visible = false;
            xrLabel493.Visible = false;
            xrLabel494.Visible = false;

            xrLabel181.Visible = false;
            xrLabel182.Visible = false;
            xrLabel183.Visible = false;
            xrLabel184.Visible = false;
            xrLabel185.Visible = false;

            xrLabel246.Visible = false;
            xrLabel247.Visible = false;
            xrLabel248.Visible = false;
            xrLabel249.Visible = false;
            xrLabel250.Visible = false;

            xrLabel310.Visible = false;
            xrLabel311.Visible = false;
            xrLabel312.Visible = false;
            xrLabel313.Visible = false;
            xrLabel314.Visible = false;

            xrLabel379.Visible = false;
            xrLabel380.Visible = false;
            xrLabel381.Visible = false;
            xrLabel382.Visible = false;
            xrLabel383.Visible = false;

            xrLabel443.Visible = false;
            xrLabel444.Visible = false;
            xrLabel445.Visible = false;
            xrLabel446.Visible = false;
            xrLabel447.Visible = false;

            xrLabel531.Visible = false;
            xrLabel532.Visible = false;
            xrLabel533.Visible = false;
            xrLabel534.Visible = false;
            xrLabel535.Visible = false;
        }

        private void setFaza3NotVisible()
        {
            xrLabel77.Visible = false;
            xrLabel79.Visible = false;
            xrLabel80.Visible = false;
            xrLabel81.Visible = false;
            xrLabel83.Visible = false;
            xrLabel84.Visible = false;

            xrLabel134.Visible = false;
            xrLabel135.Visible = false;
            xrLabel129.Visible = false;
            xrLabel128.Visible = false;
            xrLabel127.Visible = false;

            xrLabel190.Visible = false;
            xrLabel189.Visible = false;
            xrLabel188.Visible = false;
            xrLabel187.Visible = false;
            xrLabel186.Visible = false;

            xrLabel241.Visible = false;
            xrLabel242.Visible = false;
            xrLabel243.Visible = false;
            xrLabel244.Visible = false;
            xrLabel245.Visible = false;

            xrLabel305.Visible = false;
            xrLabel306.Visible = false;
            xrLabel307.Visible = false;
            xrLabel308.Visible = false;
            xrLabel309.Visible = false;

            xrLabel377.Visible = false;
            xrLabel378.Visible = false;
            xrLabel371.Visible = false;
            xrLabel370.Visible = false;
            xrLabel369.Visible = false;

            xrLabel438.Visible = false;
            xrLabel439.Visible = false;
            xrLabel440.Visible = false;
            xrLabel441.Visible = false;
            xrLabel442.Visible = false;

            xrLabel526.Visible = false;
            xrLabel527.Visible = false;
            xrLabel528.Visible = false;
            xrLabel529.Visible = false;
            xrLabel530.Visible = false;
        }

        private void setFaza2NotVisible()
        {
            faza2band.Visible = false;
            xrLabel73.Visible = false;
            xrLabel24.Visible = false;
            xrLabel23.Visible = false;
            xrLabel75.Visible = false;
            xrLabel78.Visible = false;

            xrLabel130.Visible = false;
            xrLabel131.Visible = false;
            xrLabel132.Visible = false;
            xrLabel133.Visible = false;
            xrLabel492.Visible = false;

            xrLabel66.Visible = false;
            xrLabel177.Visible = false;
            xrLabel178.Visible = false;
            xrLabel179.Visible = false;
            xrLabel180.Visible = false;

            xrLabel236.Visible = false;
            xrLabel237.Visible = false;
            xrLabel238.Visible = false;
            xrLabel239.Visible = false;
            xrLabel240.Visible = false;

            xrLabel300.Visible = false;
            xrLabel302.Visible = false;
            xrLabel301.Visible = false;
            xrLabel303.Visible = false;
            xrLabel304.Visible = false;

            xrLabel364.Visible = false;
            xrLabel365.Visible = false;
            xrLabel366.Visible = false;
            xrLabel367.Visible = false;
            xrLabel368.Visible = false;

            xrLabel433.Visible = false;
            xrLabel434.Visible = false;
            xrLabel435.Visible = false;
            xrLabel436.Visible = false;
            xrLabel437.Visible = false;

            xrLabel521.Visible = false;
            xrLabel522.Visible = false;
            xrLabel523.Visible = false;
            xrLabel524.Visible = false;
            xrLabel525.Visible = false;
        }

        private void setFaza1NotVisible()
        {
            faza1Band.Visible = false;
            xrLabel5.Visible = false;
            xrLabel8.Visible = false;
            xrLabel13.Visible = false;
            xrLabel21.Visible = false;
            xrLabel74.Visible = false;

            xrLabel173.Visible = false;
            xrLabel126.Visible = false;
            xrLabel172.Visible = false;
            xrLabel171.Visible = false;
            xrLabel170.Visible = false;

            xrLabel61.Visible = false;
            xrLabel504.Visible = false;
            xrLabel64.Visible = false;
            xrLabel62.Visible = false;
            xrLabel65.Visible = false;

            xrLabel231.Visible = false;
            xrLabel232.Visible = false;
            xrLabel233.Visible = false;
            xrLabel234.Visible = false;
            xrLabel235.Visible = false;

            xrLabel295.Visible = false;
            xrLabel299.Visible = false;
            xrLabel298.Visible = false;
            xrLabel297.Visible = false;
            xrLabel296.Visible = false;

            xrLabel359.Visible = false;
            xrLabel360.Visible = false;
            xrLabel361.Visible = false;
            xrLabel362.Visible = false;
            xrLabel363.Visible = false;

            xrLabel427.Visible = false;
            xrLabel429.Visible = false;
            xrLabel430.Visible = false;
            xrLabel431.Visible = false;
            xrLabel432.Visible = false;

            xrLabel516.Visible = false;
            xrLabel517.Visible = false;
            xrLabel518.Visible = false;
            xrLabel519.Visible = false;
            xrLabel520.Visible = false;
        }

        #endregion vendosja e labelave te fazave Visible = false

        private string emerFaze()
        {
            switch (llojFaze)
            {
                case 0:
                    return "Mujore";
                case 1:
                    return "3 Mujore";
                case 2:
                    return "4 Mujore";
                case 3:
                    return "6 Mujore";
                case 4:
                    return "12 Mujore";
                default:
                    return " ";
            }
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            

        }

        
      
    }
}
