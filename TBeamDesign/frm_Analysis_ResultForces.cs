using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.RCC_T_Girder;

namespace BridgeAnalysisDesign
{

    public partial class frm_Analysis_ResultForces : Form
    {
        IApplication iApp = null;
        BridgeMemberAnalysis BridgeDesign = null;
        public frm_Analysis_ResultForces(IApplication thisApp, string AnalysisFile)
        {
            iApp = thisApp;
            InitializeComponent();
            Read_Analysis_Result(AnalysisFile);
        }

        private void Read_Analysis_Result(string AnalysisFile)
        {
            try
            {
                BridgeDesign = new BridgeMemberAnalysis(iApp, AnalysisFile);
            }
            catch (Exception ex) { }
        }
        void Show_Moment_Shear()
        {
            MemberCollection mc = new MemberCollection(BridgeDesign.Analysis.Members);

            CMember mem = null;
            mem = new CMember();
            string kStr = "";


            MemberCollection sort_membs = new MemberCollection();

            JointNodeCollection jn_col = BridgeDesign.Analysis.Joints;

            double L = BridgeDesign.Analysis.Length;
            double W = BridgeDesign.Analysis.Width;
            double d_eff = BridgeDesign.Analysis.Effective_Depth;
            double support = BridgeDesign.Analysis.Support_Distance;


            int i = 0;

            List<int> _Support_inn_joints = new List<int>();
            List<int> _deff_inn_joints = new List<int>();
            List<int> _L8_inn_joints = new List<int>();
            List<int> _L4_inn_joints = new List<int>();
            List<int> _3L8_inn_joints = new List<int>();
            List<int> _L2_inn_joints = new List<int>();




            List<int> _Support_out_joints = new List<int>();
            List<int> _deff_out_joints = new List<int>();
            List<int> _L8_out_joints = new List<int>();
            List<int> _L4_out_joints = new List<int>();
            List<int> _3L8_out_joints = new List<int>();



            List<double> _X_joints = new List<double>();
            List<double> _Z_joints = new List<double>();

            for (i = 0; i < jn_col.Count; i++)
            {
                if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);
            }

            List<double> _X_min = new List<double>();
            List<double> _X_max = new List<double>();
            double x_max, x_min;
            double vvv = 99999999999999999;
            for (int zc = 0; zc < _Z_joints.Count; zc++)
            {

                x_min = vvv;
                x_max = -vvv;

                for (i = 0; i < jn_col.Count; i++)
                {
                    //if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                    //if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);

                    if (_Z_joints[zc] == jn_col[i].Z)
                    {
                        if (x_min > jn_col[i].X)
                            x_min = jn_col[i].X;
                        if (x_max < jn_col[i].X)
                            x_max = jn_col[i].X;
                    }

                }
                if (x_max != -vvv)
                    _X_max.Add(x_max);
                if (x_min != vvv)
                    _X_min.Add(x_min);
            }



            double cant_wi = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;


            for (i = 0; i < jn_col.Count; i++)
            {
                try
                {
                    if ((jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi)) == false) continue;
                    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];

                    #region L/2
                    if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion L/2

                    #region L/4

                    if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion L/4

                    #region support
                    if ((jn_col[i].X.ToString("0.0") == (support + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _Support_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - support + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _Support_out_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion Effective Depth

                    #region Effective Depth
                    if ((jn_col[i].X.ToString("0.0") == (d_eff + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - d_eff + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _deff_out_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion Effective Depth


                    #region L/8

                    if (jn_col[i].X.ToString("0.0") == ((L / 8.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _L8_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 8.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _L8_out_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion L/8

                    #region 3L/8

                    if (jn_col[i].X.ToString("0.0") == ((3 * L / 8.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _3L8_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (3 * L / 8.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _3L8_out_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion L/8


                }
                catch (Exception ex) { MessageBox.Show(this, ""); }
            }


            if (_L2_inn_joints.Count > 2)
            {
                _L2_inn_joints.RemoveAt(0);
                _L2_inn_joints.RemoveAt(_L2_inn_joints.Count - 1);
            }



            if (_L4_inn_joints.Count > 2)
            {
                _L4_inn_joints.RemoveAt(0);
                _L4_inn_joints.RemoveAt(_L4_inn_joints.Count - 1);
            }
            if (_L4_out_joints.Count > 2)
            {
                _L4_out_joints.RemoveAt(0);
                _L4_out_joints.RemoveAt(_L4_out_joints.Count - 1);
            }




            if (_L8_inn_joints.Count > 2)
            {
                _L8_inn_joints.RemoveAt(0);
                _L8_inn_joints.RemoveAt(_L8_inn_joints.Count - 1);
            }
            if (_L8_out_joints.Count > 2)
            {
                _L8_out_joints.RemoveAt(0);
                _L8_out_joints.RemoveAt(_L8_out_joints.Count - 1);
            }

            if (_3L8_inn_joints.Count > 2)
            {
                _3L8_inn_joints.RemoveAt(0);
                _3L8_inn_joints.RemoveAt(_3L8_inn_joints.Count - 1);
            }
            if (_3L8_out_joints.Count > 2)
            {
                _3L8_out_joints.RemoveAt(0);
                _3L8_out_joints.RemoveAt(_3L8_out_joints.Count - 1);
            }


            if (_deff_inn_joints.Count > 2)
            {
                _deff_inn_joints.RemoveAt(0);
                _deff_inn_joints.RemoveAt(_deff_inn_joints.Count - 1);
            }
            if (_deff_out_joints.Count > 2)
            {
                _deff_out_joints.RemoveAt(0);
                _deff_out_joints.RemoveAt(_deff_out_joints.Count - 1);
            }


            if (_Support_inn_joints.Count > 2)
            {
                _Support_inn_joints.RemoveAt(0);
                _Support_inn_joints.RemoveAt(_Support_inn_joints.Count - 1);
            }


            if (_Support_out_joints.Count > 2)
            {
                _Support_out_joints.RemoveAt(0);
                _Support_out_joints.RemoveAt(_Support_out_joints.Count - 1);
            }




            _Support_inn_joints.AddRange(_Support_out_joints);
            _3L8_inn_joints.AddRange(_3L8_out_joints);
            _L8_inn_joints.AddRange(_L8_out_joints);
            _L4_inn_joints.AddRange(_L4_out_joints);
            _deff_inn_joints.AddRange(_deff_out_joints);




            mem = new CMember();
            mem.Group.MemberNos = _L2_inn_joints;
            kStr = BridgeDesign.GetForce(ref mem);

            txt_inner_L2_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_inner_L2_shear_member_no.Text = mem.MaxShearForce.MemberNo.ToString();
            txt_inner_L2_shear_loadno.Text = mem.MaxShearForce.Loadcase.ToString();

            txt_inner_L2_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_inner_L2_moment_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();
            txt_inner_L2_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();



            mem = new CMember();
            mem.Group.MemberNos = _3L8_inn_joints;
            kStr = BridgeDesign.GetForce(ref mem);

            txt_inner_3L8_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_inner_3L8_shear_member_no.Text = mem.MaxShearForce.MemberNo.ToString();
            txt_inner_3L8_shear_loadno.Text = mem.MaxShearForce.Loadcase.ToString();

            txt_inner_3L8_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_inner_3L8_moment_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();
            txt_inner_3L8_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();



            mem = new CMember();
            mem.Group.MemberNos = _L4_inn_joints;
            kStr = BridgeDesign.GetForce(ref mem);

            txt_inner_L4_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_inner_L4_shear_member_no.Text = mem.MaxShearForce.MemberNo.ToString();
            txt_inner_L4_shear_loadno.Text = mem.MaxShearForce.Loadcase.ToString();

            txt_inner_L4_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_inner_L4_moment_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();
            txt_inner_L4_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();





            mem = new CMember();
            mem.Group.MemberNos = _L8_inn_joints;
            kStr = BridgeDesign.GetForce(ref mem);

            txt_inner_L8_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_inner_L8_shear_member_no.Text = mem.MaxShearForce.MemberNo.ToString();
            txt_inner_L8_shear_loadno.Text = mem.MaxShearForce.Loadcase.ToString();

            txt_inner_L8_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_inner_L8_moment_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();
            txt_inner_L8_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();





            mem = new CMember();
            mem.Group.MemberNos = _deff_inn_joints;
            kStr = BridgeDesign.GetForce(ref mem);

            txt_inner_deff_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_inner_deff_shear_member_no.Text = mem.MaxShearForce.MemberNo.ToString();
            txt_inner_deff_shear_loadno.Text = mem.MaxShearForce.Loadcase.ToString();

            txt_inner_deff_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_inner_deff_moment_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();
            txt_inner_deff_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();




            mem = new CMember();
            mem.Group.MemberNos = _Support_inn_joints;
            kStr = BridgeDesign.GetForce(ref mem);

            txt_inner_support_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_inner_shear_support_member_no.Text = mem.MaxShearForce.MemberNo.ToString();
            txt_inner_shear_support_loadno.Text = mem.MaxShearForce.Loadcase.ToString();

            txt_inner_support_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_inner_support_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();
            txt_inner_support_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();






            //txt_inner_L2_shear.Text = BridgeDesign.GetJoint_ShearForce(_L2_inn_joints).ToString();
            //txt_inner_L2_moment.Text = BridgeDesign.GetJoint_MomentForce(_L2_inn_joints).ToString();

            //txt_inner_3L8_shear.Text = BridgeDesign.GetJoint_ShearForce(_3L8_inn_joints).ToString();
            //txt_inner_3L8_moment.Text = BridgeDesign.GetJoint_MomentForce(_3L8_inn_joints).ToString();

            //txt_inner_L4_shear.Text = BridgeDesign.GetJoint_ShearForce(_L4_inn_joints).ToString();
            //txt_inner_L4_moment.Text = BridgeDesign.GetJoint_MomentForce(_L4_inn_joints).ToString();

            //txt_inner_L8_shear.Text = BridgeDesign.GetJoint_ShearForce(_L8_inn_joints).ToString();
            //txt_inner_L8_moment.Text = BridgeDesign.GetJoint_MomentForce(_L8_inn_joints).ToString();

            //txt_inner_deff_shear.Text = BridgeDesign.GetJoint_ShearForce(_deff_inn_joints).ToString();
            //txt_inner_deff_moment.Text = BridgeDesign.GetJoint_MomentForce(_deff_inn_joints).ToString();

            //txt_inner_support_shear.Text = BridgeDesign.GetJoint_ShearForce(_Support_inn_joints).ToString();
            //txt_inner_support_moment.Text = BridgeDesign.GetJoint_MomentForce(_Support_inn_joints).ToString();




            _L2_inn_joints.Clear();
            _3L8_inn_joints.Clear();
            _L4_inn_joints.Clear();
            _L8_inn_joints.Clear();
            _deff_inn_joints.Clear();
            _Support_inn_joints.Clear();

            _3L8_out_joints.Clear();
            _L8_out_joints.Clear();
            _L4_out_joints.Clear();
            _deff_out_joints.Clear();
            _Support_out_joints.Clear();


            for (i = 0; i < jn_col.Count; i++)
            {
                try
                {
                    if ((jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi)) == false) continue;
                    x_min = _X_min[_Z_joints.IndexOf(jn_col[i].Z)];

                    #region L/2
                    if ((jn_col[i].X.ToString("0.0") == ((L / 2.0) + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi && jn_col[i].Z <= (W - cant_wi))
                            _L2_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion L/2

                    #region L/4

                    if (jn_col[i].X.ToString("0.0") == ((L / 4.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _L4_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 4.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _L4_out_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion L/4

                    #region support
                    if ((jn_col[i].X.ToString("0.0") == (support + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _Support_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - support + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _Support_out_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion Effective Depth

                    #region Effective Depth
                    if ((jn_col[i].X.ToString("0.0") == (d_eff + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _deff_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if ((jn_col[i].X.ToString("0.0") == (L - d_eff + x_min).ToString("0.0")))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _deff_out_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion Effective Depth


                    #region L/8

                    if (jn_col[i].X.ToString("0.0") == ((L / 8.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _L8_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (L / 8.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _L8_out_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion L/8

                    #region 3L/8

                    if (jn_col[i].X.ToString("0.0") == ((3 * L / 8.0) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z >= cant_wi)
                            _3L8_inn_joints.Add(jn_col[i].NodeNo);
                    }
                    if (jn_col[i].X.ToString("0.0") == ((L - (3 * L / 8.0)) + x_min).ToString("0.0"))
                    {
                        if (jn_col[i].Z <= (W - cant_wi))
                            _3L8_out_joints.Add(jn_col[i].NodeNo);
                    }
                    #endregion L/8
                }
                catch (Exception ex) { MessageBox.Show(this, ""); }
            }
            if (_L2_inn_joints.Count > 2)
            {
                _L2_inn_joints.RemoveRange(1, _L2_inn_joints.Count - 2);
            }

            if (_L4_inn_joints.Count > 2)
            {
                _L4_inn_joints.RemoveRange(1, _L4_inn_joints.Count - 2);
                _L4_out_joints.RemoveRange(1, _L4_out_joints.Count - 2);
                _L4_out_joints.AddRange(_L4_inn_joints);
            }

            if (_deff_inn_joints.Count > 2)
            {

                _deff_inn_joints.RemoveRange(1, _deff_inn_joints.Count - 2);
                _deff_out_joints.RemoveRange(1, _deff_out_joints.Count - 2);
                _deff_out_joints.AddRange(_deff_inn_joints);

            }
            if (_Support_inn_joints.Count > 2)
            {
                _Support_inn_joints.RemoveRange(1, _Support_inn_joints.Count - 2);
                _Support_out_joints.RemoveRange(1, _Support_out_joints.Count - 2);
                _Support_out_joints.AddRange(_Support_inn_joints);
            }

            if (_L8_inn_joints.Count > 2)
            {
                _L8_inn_joints.RemoveRange(1, _L8_inn_joints.Count - 2);
                _L8_out_joints.RemoveRange(1, _L8_out_joints.Count - 2);
                _L8_out_joints.AddRange(_L8_inn_joints);
            }

            if (_3L8_inn_joints.Count > 2)
            {
                _3L8_inn_joints.RemoveRange(1, _3L8_inn_joints.Count - 2);
                _3L8_out_joints.RemoveRange(1, _3L8_out_joints.Count - 2);
                _3L8_out_joints.AddRange(_3L8_inn_joints);
            }


            mem = new CMember();
            mem.Group.MemberNos = _Support_out_joints;
            kStr = BridgeDesign.GetForce(ref mem);

            txt_outer_support_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_outer_support_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();
            txt_outer_support_moment_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();


            txt_outer_support_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_outer_support_shear_loadno.Text = mem.MaxShearForce.Loadcase.ToString();
            txt_outer_support_shear_member_no.Text = mem.MaxShearForce.MemberNo.ToString();






            mem = new CMember();
            mem.Group.MemberNos = _deff_out_joints;
            kStr = BridgeDesign.GetForce(ref mem);

            txt_outer_deff_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_outer_deff_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();
            txt_outer_deff_moment_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();

            txt_outer_deff_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_outer_deff_shear_loadno.Text = mem.MaxShearForce.Force.ToString();
            txt_outer_deff_shear_member_no.Text = mem.MaxShearForce.Force.ToString();








            mem = new CMember();
            mem.Group.MemberNos = _L8_out_joints;
            kStr = BridgeDesign.GetForce(ref mem);


            txt_outer_L8_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_outer_L8_moment_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();
            txt_outer_L8_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();

            txt_outer_L8_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_outer_L8_shear_member_no.Text = mem.MaxShearForce.MemberNo.ToString();
            txt_outer_L8_shear_loadno.Text = mem.MaxShearForce.Loadcase.ToString();








            mem = new CMember();
            mem.Group.MemberNos = _L4_out_joints;
            kStr = BridgeDesign.GetForce(ref mem);

            txt_outer_L4_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_outer_L4_moment_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();
            txt_outer_L4_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();

            txt_outer_L4_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_outer_L4_shear_member_no.Text = mem.MaxShearForce.MemberNo.ToString();
            txt_outer_L4_shear_loadno.Text = mem.MaxShearForce.Loadcase.ToString();





            mem = new CMember();
            mem.Group.MemberNos = _3L8_out_joints;
            kStr = BridgeDesign.GetForce(ref mem);


            txt_outer_3L8_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_outer_3L8_moment_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();
            txt_outer_3L8_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();

            txt_outer_3L8_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_outer_3L8_shear_member_no.Text = mem.MaxShearForce.MemberNo.ToString();
            txt_outer_3L8_shear_loadno.Text = mem.MaxShearForce.Loadcase.ToString();




            mem = new CMember();
            mem.Group.MemberNos = _L2_inn_joints;
            kStr = BridgeDesign.GetForce(ref mem);


            txt_outer_L2_moment.Text = mem.MaxBendingMoment.Force.ToString();
            txt_outer_L2_moment_loadno.Text = mem.MaxBendingMoment.Loadcase.ToString();
            txt_outer_L2_moment_member_no.Text = mem.MaxBendingMoment.MemberNo.ToString();
            txt_outer_L2_shear.Text = mem.MaxShearForce.Force.ToString();
            txt_outer_L2_shear_loadno.Text = mem.MaxShearForce.Loadcase.ToString();
            txt_outer_L2_shear_member_no.Text = mem.MaxShearForce.MemberNo.ToString();

            //CMember mem = new CMember();
            //mem.Group.MemberNos = _L2_inn_joints;


            //txt_outer_L2_shear.Text = BridgeDesign.GetForce(ref mem);
            //txt_outer_L2_shear.Text = mem.Force.ToString();
            //txt_outer_L2_shear_member_no.Text = mem.MaxShearForce.MemberNo.ToString();
            //txt_outer_L2_shear_node_no.Text = mem.MaxShearForce.Loadcase.ToString();




            #region Null All variables
            mc = null;


            jn_col = null;


            _L2_inn_joints = null;
            _L4_inn_joints = null;
            _deff_inn_joints = null;

            _L4_out_joints = null;
            _deff_out_joints = null;
            #endregion
            Show_Cross_Girder();
        }
        void Show_Cross_Girder()
        {
            MemberCollection mc = new MemberCollection(BridgeDesign.Analysis.Members);

            MemberCollection sort_membs = new MemberCollection();

            double z_min = double.MaxValue;
            double x = double.MaxValue;
            int indx = -1;

            int i = 0;
            int j = 0;

            List<double> list_z = new List<double>();

            List<MemberCollection> list_mc = new List<MemberCollection>();

            double last_z = 0.0;
            //double z_min = double.MaxValue;

            while (mc.Count != 0)
            {
                indx = -1;
                for (i = 0; i < mc.Count; i++)
                {
                    if (z_min > mc[i].StartNode.Z)
                    {
                        z_min = mc[i].StartNode.Z;
                        indx = i;
                    }
                }
                if (indx != -1)
                {

                    if (!list_z.Contains(z_min))
                        list_z.Add(z_min);

                    sort_membs.Add(mc[indx]);
                    mc.Members.RemoveAt(indx);
                    z_min = double.MaxValue;
                }
            }



            List<string> list_arr = new List<string>();


            last_z = -1.0;



            //Outer Long Girder
            MemberCollection outer_long = new MemberCollection();
            MemberCollection inner_long = new MemberCollection();
            MemberCollection inner_cross = new MemberCollection();


            //z_min = Truss_Analysis.Analysis.Joints.MinZ;
            //double z_max = Truss_Analysis.Analysis.Joints.MaxZ;

            //Chiranjit [2011 07 09]
            //Store Outer Girder Members
            int count = 0;
            z_min = 0.0;
            for (i = 0; i < sort_membs.Count; i++)
            {
                if (z_min < sort_membs[i].StartNode.Z)
                {
                    z_min = sort_membs[i].StartNode.Z;
                    count++;
                }
                if (z_min < sort_membs[i].EndNode.Z)
                {
                    z_min = sort_membs[i].EndNode.Z;
                    count++;
                }
                //For Outer Girder
                if (count == 2) break;
                //if (count == 0) break;
            }

            //z_min = WidthCantilever;
            double z_max = z_min;


            //Store inner and outer Long Girder
            for (i = 0; i < sort_membs.Count; i++)
            {
                if (((sort_membs[i].StartNode.Z == z_min) || (sort_membs[i].StartNode.Z == z_max)) &&
                    sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
                {
                    outer_long.Add(sort_membs[i]);
                }
                else if (((sort_membs[i].StartNode.Z != z_min) && (sort_membs[i].StartNode.Z != z_max)) &&
                    sort_membs[i].StartNode.Z == sort_membs[i].EndNode.Z)
                {
                    inner_long.Add(sort_membs[i]);
                }
            }

            //Store Cross Girders
            for (i = 0; i < sort_membs.Count; i++)
            {
                if (outer_long.Contains(sort_membs[i]) == false &&
                    inner_long.Contains(sort_membs[i]) == false)
                {
                    inner_cross.Add(sort_membs[i]);
                }
            }




            //Find X MIN    X MAX   for outer long girder
            double x_min, x_max;

            List<double> list_outer_xmin = new List<double>();
            List<double> list_inner_xmin = new List<double>();
            List<double> list_inner_cur_z = new List<double>();
            List<double> list_outer_cur_z = new List<double>();

            List<double> list_outer_xmax = new List<double>();
            List<double> list_inner_xmax = new List<double>();


            x_min = double.MaxValue;
            x_max = double.MinValue;

            last_z = outer_long[0].StartNode.Z;
            for (i = 0; i < outer_long.Count; i++)
            {
                if (last_z == outer_long[i].StartNode.Z)
                {
                    if (x_min > outer_long[i].StartNode.X)
                    {
                        x_min = outer_long[i].StartNode.X;
                    }
                    if (x_max < outer_long[i].EndNode.X)
                    {
                        x_max = outer_long[i].EndNode.X;
                    }
                }
                else
                {
                    list_outer_xmax.Add(x_max);
                    list_outer_xmin.Add(x_min);
                    list_outer_cur_z.Add(last_z);

                    x_min = outer_long[i].StartNode.X;
                    x_max = outer_long[i].EndNode.X;


                }
                last_z = outer_long[i].StartNode.Z;
            }

            list_outer_xmax.Add(x_max);
            list_outer_xmin.Add(x_min);
            list_outer_cur_z.Add(last_z);

            x_min = double.MaxValue;
            x_max = double.MinValue;

            last_z = inner_long.Count > 0 ? inner_long[0].StartNode.Z : 0.0;

            for (i = 0; i < inner_long.Count; i++)
            {
                if (last_z == inner_long[i].StartNode.Z)
                {
                    if (x_min > inner_long[i].StartNode.X)
                    {
                        x_min = inner_long[i].StartNode.X;
                    }
                    if (x_max < inner_long[i].EndNode.X)
                    {
                        x_max = inner_long[i].EndNode.X;
                    }
                }
                else
                {
                    list_inner_xmax.Add(x_max);
                    list_inner_xmin.Add(x_min);
                    list_inner_cur_z.Add(last_z);

                    x_min = inner_long[i].StartNode.X;
                    x_max = inner_long[i].EndNode.X;

                }
                last_z = inner_long[i].StartNode.Z;
            }

            list_inner_xmax.Add(x_max);
            list_inner_xmin.Add(x_min);

            list_inner_cur_z.Add(last_z);

            List<int> _deff_joints = new List<int>();
            List<int> _L_4_joints = new List<int>();
            List<int> _L_2_joints = new List<int>();
            //Member Forces from Report for Inner girder


            //int cur_node = -1;
            int cur_member = -1;
            // FOR L/2
            string curr_membs_L2_text = "";
            // FOR L/4
            string curr_membs_L4_text = "";
            //FOR Effective Depth
            string curr_membs_Deff_text = "";


            double cur_z = 0.0;
            double cur_y = 0.0;

            double curr_L2_x = 0.0;
            double curr_L4_x = 0.0;
            double curr_Deff_x = 0.0;

            curr_membs_L2_text = "";
            curr_membs_L4_text = "";
            curr_membs_Deff_text = "";
            cur_member = -1;

            //if (outer_long.Count > 0)
            //    Bridge_Analysis.Effective_Depth = outer_long[0].Length;

            for (i = 0; i < list_inner_xmax.Count; i++)
            {
                x_max = list_inner_xmax[i];
                x_min = list_inner_xmin[i];

                cur_z = list_inner_cur_z[i];

                curr_L2_x = (x_max + x_min) / 2.0;
                curr_L4_x = (curr_L2_x + x_min) / 2.0;
                curr_Deff_x = (BridgeDesign.Analysis.Effective_Depth + x_min);

                cur_y = 0.0;

                for (j = 0; j < inner_long.Count; j++)
                {
                    if ((inner_long[j].EndNode.Y.ToString("0.0") == cur_y.ToString("0.0")) &&
                        (inner_long[j].EndNode.Z.ToString("0.0") == cur_z.ToString("0.0")))
                    {
                        if ((inner_long[j].EndNode.X.ToString("0.0") == curr_L2_x.ToString("0.0")))
                        {
                            cur_member = inner_long[j].MemberNo;
                            curr_membs_L2_text += cur_member + " ";
                            _L_2_joints.Add(inner_long[j].EndNode.NodeNo);
                        }
                        else if ((inner_long[j].EndNode.X.ToString("0.0") == curr_L4_x.ToString("0.0")))
                        {
                            cur_member = inner_long[j].MemberNo;
                            curr_membs_L4_text += cur_member + " ";
                            _L_4_joints.Add(inner_long[j].EndNode.NodeNo);
                        }
                        else if ((inner_long[j].EndNode.X.ToString("0.0") == curr_Deff_x.ToString("0.0")))
                        {
                            cur_member = inner_long[j].MemberNo;
                            curr_membs_Deff_text += cur_member + " ";
                            _deff_joints.Add(inner_long[j].EndNode.NodeNo);
                        }
                    }
                }
            }


            _L_2_joints.Remove(64);
            _L_4_joints.Remove(42);
            _deff_joints.Remove(20);

            //For Outer Long Girder
            curr_membs_L2_text = "";
            curr_membs_L4_text = "";
            curr_membs_Deff_text = "";
            cur_member = -1;
            _deff_joints.Clear();
            _L_2_joints.Clear();
            _L_4_joints.Clear();
            //Creating X Coordinates at every Z level
            for (i = 0; i < list_outer_xmax.Count; i++)
            {
                x_max = list_outer_xmax[i];
                x_min = list_outer_xmin[i];

                cur_z = list_outer_cur_z[i];

                curr_L2_x = (x_max + x_min) / 2.0;
                curr_L4_x = (curr_L2_x + x_min) / 2.0;
                curr_Deff_x = (BridgeDesign.Analysis.Effective_Depth + x_min);

                cur_y = 0.0;

                for (j = 0; j < outer_long.Count; j++)
                {
                    if ((outer_long[j].EndNode.Y.ToString("0.0") == cur_y.ToString("0.0")) &&
                        (outer_long[j].EndNode.Z.ToString("0.0") == cur_z.ToString("0.0")))
                    {
                        if ((outer_long[j].EndNode.X.ToString("0.0") == curr_L2_x.ToString("0.0")))
                        {
                            cur_member = outer_long[j].MemberNo;
                            curr_membs_L2_text += cur_member + " ";
                            _L_2_joints.Add(outer_long[j].EndNode.NodeNo);

                        }
                        else if ((outer_long[j].EndNode.X.ToString("0.0") == curr_L4_x.ToString("0.0")))
                        {
                            cur_member = outer_long[j].MemberNo;
                            curr_membs_L4_text += cur_member + " ";
                            _L_4_joints.Add(outer_long[j].EndNode.NodeNo);
                        }
                        else if ((outer_long[j].EndNode.X.ToString("0.0") == curr_Deff_x.ToString("0.0")))
                        {
                            cur_member = outer_long[j].MemberNo;
                            curr_membs_Deff_text += cur_member + " ";
                            _deff_joints.Add(outer_long[j].EndNode.NodeNo);
                        }
                    }
                }
            }



            //Cross Girder
            string cross_text = "";
            if (inner_cross.Count == 0)
                MessageBox.Show(this, "No Cross Girder was found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {

                for (j = 0; j < inner_cross.Count; j++)
                {

                    cur_member = inner_cross[j].MemberNo;
                    cross_text += cur_member + " ";
                }

                CMember m = new CMember();
                m.Group.MemberNosText = cross_text;
                m.Force = BridgeDesign.GetForce(ref m);

                txt_cross_moment.Text = (m.MaxBendingMoment).ToString();
                txt_cross_shear.Text = m.MaxShearForce.ToString();
            }
        }

        private void frm_Analysis_ResultForces_Load(object sender, EventArgs e)
        {
            Show_Moment_Shear();
            //Show_Cross_Girder();
        }
    }
}
