namespace Timetable.timetable.DB
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	public partial class HR_MasterData_Employees
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public HR_MasterData_Employees()
		{
			HR_MasterData_Employees1 = new HashSet<HR_MasterData_Employees>();
			School_Lookup_Class = new HashSet<School_Lookup_Class>();
			School_TeacherSubjects = new HashSet<School_TeacherSubjects>();
		}

		[Key]
		public long EmployeeID { get; set; }

		[StringLength(300)]
		public string Employee_Name_Ar { get; set; }

		[StringLength(300)]
		public string Employee_Name_En { get; set; }

		public long? UserID { get; set; }

		[StringLength(50)]
		public string ShortName { get; set; }

		[StringLength(100)]
		public string Title { get; set; }

		public int? CountryID { get; set; }

		public int? Religion { get; set; }

		[Column(TypeName = "date")]
		public DateTime? BirthDate { get; set; }

		public int? BirthRegionID { get; set; }

		[Column(TypeName = "date")]
		public DateTime? NationalIdentyStartDate { get; set; }

		[Column(TypeName = "date")]
		public DateTime? NationalIdentyEndDate { get; set; }

		[StringLength(150)]
		public string PassportNumber { get; set; }

		[Column(TypeName = "date")]
		public DateTime? PassportStart { get; set; }

		[Column(TypeName = "date")]
		public DateTime? PassportEnd { get; set; }

		public int? MilitaryStatus { get; set; }

		public string MilitaryComment { get; set; }

		public bool? Gender { get; set; }

		[StringLength(300)]
		public string PersonalImage { get; set; }

		public int? MaritalStatus { get; set; }

		public int? ChildrenNumber { get; set; }

		public string Hobbies { get; set; }

		public string Skills { get; set; }

		public int? HealthCaseID { get; set; }

		public string Notes { get; set; }

		public string HealthNotes { get; set; }

		public int? BloodType { get; set; }

		[StringLength(50)]
		public string MobileNumber { get; set; }

		[StringLength(50)]
		public string MobileNumber2 { get; set; }

		[StringLength(50)]
		public string Phone { get; set; }

		[StringLength(50)]
		public string Fax { get; set; }

		[StringLength(350)]
		public string Email { get; set; }

		public string Address { get; set; }

		public int? Address_CityID { get; set; }

		public int? Address_RegionID { get; set; }

		[StringLength(50)]
		public string Work_Mobile { get; set; }

		[StringLength(50)]
		public string Work_Mobile2 { get; set; }

		[StringLength(50)]
		public string Work_phone { get; set; }

		[StringLength(50)]
		public string Work_Fax { get; set; }

		[StringLength(350)]
		public string Work_Email { get; set; }

		public int? AttenRulesID { get; set; }

		public int? WorkTimeID { get; set; }

		public double? BasicSalary { get; set; }

		public double? FinalSalary { get; set; }

		public int? WorkDays { get; set; }

		public double? WorkHours { get; set; }

		public int? PartTimeAccountID { get; set; }

		public double? PartTimeValue { get; set; }

		public bool? IsInsurance { get; set; }

		public bool? IsSupervisor { get; set; }

		public int? DepartmentID { get; set; }

		public bool? IsTeacher { get; set; }

		public long? SupervisorID { get; set; }

		[StringLength(150)]
		public string FatherName_Ar { get; set; }

		[StringLength(150)]
		public string FatherName_En { get; set; }

		[StringLength(150)]
		public string lastName_Ar { get; set; }

		[StringLength(150)]
		public string lastName_En { get; set; }

		[StringLength(150)]
		public string FirstName_Ar { get; set; }

		[StringLength(150)]
		public string FirstName_En { get; set; }

		[StringLength(150)]
		public string EmergencyName { get; set; }

		[StringLength(150)]
		public string EmergencyMobile { get; set; }

		[StringLength(150)]
		public string EmergencyPhone { get; set; }

		[StringLength(150)]
		public string EmergencyEmail { get; set; }

		[StringLength(150)]
		public string EmergencyAddress { get; set; }



		public bool? IsActive { get; set; }

		public int? JobType { get; set; }
              
		public double? BasicValue { get; set; }

		public double? BasicPersent { get; set; }

		public double? BasicFinal { get; set; }

		public double? VariableValue { get; set; }

		public double? VariablePercent { get; set; }

		public double? VariableFinal { get; set; }

		public bool? IsSupervisorFloor { get; set; }

		public bool? IsSupervisorSubject { get; set; }

		public int? SectionType { get; set; }
        
     
		public int? ISspecialized { get; set; }


		[StringLength(100)]
		public string TotalWorkedTime { get; set; }
  
		public int? TotalWorkDays { get; set; }

		public int timeOffConstraint { get; set; }


		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<HR_MasterData_Employees> HR_MasterData_Employees1 { get; set; }

		public virtual HR_MasterData_Employees HR_MasterData_Employees2 { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<School_Lookup_Class> School_Lookup_Class { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<School_TeacherSubjects> School_TeacherSubjects { get; set; }
	}
}
