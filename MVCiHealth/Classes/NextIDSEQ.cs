using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCiHealth.Models;
using System.Linq.Expressions;

namespace MVCiHealth
{
    public partial class Global
    {
        static iHealthEntities db = new iHealthEntities();

        public static int NextUserID()
        {
            try
            {
                return db.USERINFO.Select(m => m.USER_ID).Max() + 1;
            }
            catch
            {
                return 1;
            }
        }
        public static int NextGroupID()
        {
            try
            {
                return db.GROUPINFO.Select(m => m.GROUP_ID).Max() + 1;
            }
            catch
            {
                return 1;
            }
        }
        public static int NextSysLogID()
        {
            try
            {
                return db.SYSLOG.Select(m => m.LOG_ID).Max() + 1;
            }
            catch
            {
                return 1;
            }
        }
        public static int NextReservationID()
        {
            try
            {
                return db.RESERVATION.Select(m => m.RESERVATION_ID).Max() + 1;
            }
            catch
            {
                return 1;
            }
        }
        public static int NextEvaluationID()
        {
            try
            {
                return db.DOCTOR_EVALUATION.Select(m => m.EVALUATION_ID).Max() + 1;
            }
            catch
            {
                return 1;
            }
        }
        public static int NextHistoryID()
        {
            try
            {
                return db.PATIENT_HISTORY.Select(m => m.HISTORY_ID).Max() + 1;
            }
            catch
            {
                return 1;
            }
        }
        public static int NextSectionTypeID()
        {
            try
            {
                return db.SECTION_TYPE.Select(m => m.SECTION_ID).Max() + 1;
            }
            catch
            {
                return 1;
            }
        }
    }
}