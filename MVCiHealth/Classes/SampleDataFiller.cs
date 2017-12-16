using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCiHealth.Models;

namespace MVCiHealth.Utils
{
    public static class SampleDataFiller
    {
        static iHealthEntities db = new iHealthEntities();
        public static void FillData(int count = 10)
        {
            try
            {
                //添加 GROUPINFO
                try
                {
                    var i = 0;
                    foreach (GroupType item in Enum.GetValues(typeof(GroupType)))
                    {
                        db.GROUPINFO.Add(new GROUPINFO()
                        {
                            GROUP_ID = ++i,
                            GROUP_NM = item.ToString(),
                            AUTHENTICATION = (int)item,
                            INSDATE = DateTime.Now
                        });
                    }
                    db.SaveChanges();
                }
                catch { }
                //添加 USERINFO
                try
                {
                    for (int i = 1; i <= count; i++)//病人
                    {
                        db.USERINFO.Add(new USERINFO()
                        {
                            USER_ID = i,
                            LOGIN_NM = "test" + i.ToString(),
                            GROUP_ID = (int)GroupType.Patient,
                            PASSWORD = "a12345",
                            VALID = "T",
                            INSDATE = DateTime.Now,
                        });
                    }
                    db.SaveChanges();
                }
                catch { }
                try
                {
                    for (int i = count + 1; i <= count * 2; i++)//医生
                    {
                        db.USERINFO.Add(new USERINFO()
                        {
                            USER_ID = i,
                            LOGIN_NM = "test" + i.ToString(),
                            GROUP_ID = (int)GroupType.Doctor,
                            PASSWORD = "a12345",
                            VALID = "T",
                            INSDATE = DateTime.Now,
                        });
                    }
                    db.SaveChanges();
                }
                catch { }
                //添加 PATIENT
                try
                {
                    for (int i = 1; i <= count; i++)
                    {
                        db.PATIENT.Add(new PATIENT()
                        {
                            PATIENT_ID = i,
                            PATIENT_NM = "病人姓名" + i.ToString(),
                            AGE = 17 + i,
                            GENDER = i % 3,
                            INSDATE = DateTime.Now,
                        });
                    }
                    db.SaveChanges();
                }
                catch { }
                //添加 DOCTOR
                try
                {
                    for (int i = 1; i <= count; i++)
                    {
                        db.DOCTOR.Add(new DOCTOR()
                        {
                            DOCTOR_ID = i + count,
                            DOCTOR_NM = "医生姓名" + i.ToString(),
                            AGE = 17 + i,
                            GENDER = i % 3,
                            LEVEL = i % 6,
                            INSDATE = DateTime.Now,
                        });
                    }
                    db.SaveChanges();
                }
                catch { }
                //添加 SECTION_TYPE
                try
                {
                    for (int i = 1; i <= count; i++)
                    {
                        db.SECTION_TYPE.Add(new SECTION_TYPE()
                        {
                            SECTION_ID = i,
                            SECTION_NM = "第" + i + "科室",
                            SECTION_OID = i,
                        });
                    }
                    db.SaveChanges();
                }
                catch { }
                //添加 RESERVATION
                try
                {
                    for (int i = 1; i <= count; i++)
                    {
                        db.RESERVATION.Add(new RESERVATION()
                        {
                            RESERVATION_ID = i,
                            DOCTOR_ID = count + 1,
                            PATIENT_ID = i,
                            TIME_START = DateTime.Now.AddDays(1),
                            TIME_FINISH = DateTime.Now.AddDays(1.5),
                            CONFIRMED = i % 2 == 0 ? "F" : "T",
                            VALID = "T",
                            INSDATE = DateTime.Now,
                        });
                    }
                    db.SaveChanges();
                }
                catch { }
                //添加 PATIENT_HISTORY
                try
                {
                    for (int i = 1; i <= count; i++)
                    {
                        db.PATIENT_HISTORY.Add(new PATIENT_HISTORY()
                        {
                            HISTORY_ID = i,
                            PATIENT_ID = i % 2,
                            HISTORY_URL = "/Resources/Docs/" + (i % 2) + "/history.doc",
                            PATIENT_IN = "F",
                            INSDATE = DateTime.Now,
                        });
                    }
                    db.SaveChanges();
                }
                catch { }
                //添加 DOCTOR_EVALUATION
                try
                {
                    for (int i = 1; i <= count; i++)
                    {
                        db.DOCTOR_EVALUATION.Add(new DOCTOR_EVALUATION()
                        {
                            EVALUATION_ID = i,
                            PATIENT_ID = i % 2,
                            DOCTOR_ID = count + 1,
                            RATE = 4,
                            DETAIL = "Nice work " + i,
                            AGREETIMES = i,
                            INSDATE = DateTime.Now,
                        });
                    }
                    db.SaveChanges();
                }
                catch { }
                //添加 DISEASE_TYPE
                try
                {
                    for (int i = 1; i <= count; i++)
                    {
                        db.DISEASE_TYPE.Add(new DISEASE_TYPE()
                        {
                            DISEASE_ID = i,
                            DISEASE_NM = "疾病" + i,
                            DISEASE_DESCRIPTION = "疾病说明" + i,
                            SECTION_ID = i
                        });
                    }
                    db.SaveChanges();
                }
                catch { }
            }
            catch { }
        }
    }
}