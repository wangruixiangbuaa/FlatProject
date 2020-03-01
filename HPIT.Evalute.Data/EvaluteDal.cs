using HPIT.Data.Core;
using HPIT.Evalute.Data.Model;
using HPIT.StudentEvaluate.Core;
using System.Collections.Generic;
using System.Linq;

namespace HPIT.Evalute.Data
{
    public class EvaluteDal
    {
        public static EvaluteDal Instance = new EvaluteDal();
        public EvalStudent GetMatchStudent(string name, string classNo="")
        {
            string sql = @"select * from (
                           select s.StudentNo,m.Name as mName,year(b.CheckInTime) as bYear,
                           s.Name,s.Mobile,s.[Address],s.EducationBackground as GraduateSchool ,p.Name as pName,isnull(p.periodType,1) as periodType,
                           (select RealName from Member me where me.Id = p.PeopleManagerId) as PEM,
                           (select RealName from Member me where me.Id = p.ProjectManagerId) as PRM,
                           p.Classroom,b.Name as bName from Student s 
                           left join ProjectDepartment p on s.ProjectDepartmentId = p.Id 
                           left join Batch b on p.BatchId = b.Id
                           left join Major m on p.MajorId = m.Id 
                           ) t where Name=@Name or pName=@ClassName";
            EvalStudent stu = new EvalStudent();
            stu.Name = name;
            stu.ClassName = classNo;
            EvalStudent result = DapperDBHelper.Instance.ExcuteQuery<EvalStudent>(sql, stu).ToList().FirstOrDefault();
            return result;
        }


        public List<EvalStudent> GetStudentsByBatchs(string batch,string direction,string projectName)
        {
            string sql = @"select * from (
                           select s.StudentNo,m.Name as mName,year(b.CheckInTime) as bYear,
                           s.Name,s.Mobile,s.[Address],s.EducationBackground as GraduateSchool ,p.Name as pName,isnull(p.periodType,1) as periodType,
                           (select RealName from Member me where me.Id = p.PeopleManagerId) as PEM,
                           (select RealName from Member me where me.Id = p.ProjectManagerId) as PRM,
                           p.Classroom,b.Name as bName from Student s 
                           left join ProjectDepartment p on s.ProjectDepartmentId = p.Id 
                           left join Batch b on p.BatchId = b.Id
                           left join Major m on p.MajorId = m.Id 
                           ) t where t.bName = @bName";
            if (!string.IsNullOrEmpty(direction))
            {
                sql += " and t.mName = @mName";
            }
            if (!string.IsNullOrEmpty(projectName))
            {
                sql += " and t.pName = @pName";
            }
            EvalStudent stu = new EvalStudent();
            stu.bName = batch;
            stu.mName = direction;
            stu.pName = projectName;
            List<EvalStudent> result = DapperDBHelper.Instance.ExcuteQuery<EvalStudent>(sql, stu).OrderBy(r=>r.Name).ToList();
            return result;
        }


        public List<string> GetBatchs()
        {
            string sql = @"select * from (
                           select s.StudentNo,m.Name as mName,year(b.CheckInTime) as bYear,
                           s.Name,s.Mobile,s.[Address],s.EducationBackground as GraduateSchool ,p.Name as pName,
                           (select RealName from Member me where me.Id = p.PeopleManagerId) as PEM,
                           (select RealName from Member me where me.Id = p.ProjectManagerId) as PRM,
                           p.Classroom,b.Name as bName from Student s 
                           left join ProjectDepartment p on s.ProjectDepartmentId = p.Id 
                           left join Batch b on p.BatchId = b.Id
                           left join Major m on p.MajorId = m.Id 
                           ) t";
            EvalStudent stu = new EvalStudent();
            List<EvalStudent> result = DapperDBHelper.Instance.ExcuteQuery<EvalStudent>(sql, stu).ToList();
            List<string> data = result.Where(r=>!string.IsNullOrEmpty(r.bName)).Select(r => r.bName).Distinct().ToList();
            return data;
        }



        public List<string> GetProjectNames()
        {
            string sql = @"select * from (
                           select s.StudentNo,m.Name as mName,year(b.CheckInTime) as bYear,
                           s.Name,s.Mobile,s.[Address],s.EducationBackground as GraduateSchool ,p.Name as pName,
                           (select RealName from Member me where me.Id = p.PeopleManagerId) as PEM,
                           (select RealName from Member me where me.Id = p.ProjectManagerId) as PRM,
                           p.Classroom,b.Name as bName from Student s 
                           left join ProjectDepartment p on s.ProjectDepartmentId = p.Id 
                           left join Batch b on p.BatchId = b.Id
                           left join Major m on p.MajorId = m.Id 
                           ) t";
            EvalStudent stu = new EvalStudent();
            List<EvalStudent> result = DapperDBHelper.Instance.ExcuteQuery<EvalStudent>(sql, stu).ToList();
            List<string> data = result.Where(r => !string.IsNullOrEmpty(r.pName)).Select(r => r.pName).Distinct().ToList();
            return data;
        }

        public List<EvalStudent> GetMatchStudentByNo(string StudentNo)
        {
            string sql = @"select * from (
                          select s.StudentNo,m.Name as mName,year(b.CheckInTime) as bYear,
                          s.Name,s.Mobile,s.[Address],s.GraduateSchool,p.Name as pName,
                          (select RealName from Member me where me.Id = p.PeopleManagerId) as PEM,
                          (select RealName from Member me where me.Id = p.ProjectManagerId) as PRM,
                          p.Classroom,b.Name as bName from Student s 
                          left join ProjectDepartment p on s.ProjectDepartmentId = p.Id 
                          left join Batch b on p.BatchId = b.Id
                          left join Major m on p.MajorId = m.Id ) t where StudentNo=@StudentNo";
            EvalStudent stu = new EvalStudent();
            stu.StudentNo = StudentNo;
            List<EvalStudent> result = DapperDBHelper.Instance.ExcuteQuery<EvalStudent>(sql, stu).ToList();
            return result;
        }


        public List<HPITMemberInfo> LoginMember(string loginName, string passWord)
        {
            string sql = @"select * from(  select m.RealName,m.Mobile,m.Email,r.Category,r.FullName,r.Description,r.OrganizeId 
                          ,o.FullName as oFullName,o.Address,o.Manager,o.Mobile as oMobile,ma.Password
                          from Member m 
                          left join RoleInfo r on m.RoleId = r.Id
                          left join Organize o on m.OrganizeId = o.Id
                          left join MemberAccount ma on ma.Id = m.MemberAccountId) t where t.RealName = @RealName and t.Password = @Password";
            HPITMemberInfo stu = new HPITMemberInfo();
            stu.RealName = loginName;

            stu.Password =Md5Encrypt.Md5(passWord);
            LogHelper.Default.WriteInfo(stu.RealName+"---"+stu.Password);
            List<HPITMemberInfo> result = DapperDBHelper.Instance.ExcuteQuery<HPITMemberInfo>(sql, stu).ToList();
           

            return result;
        }
    }
}
