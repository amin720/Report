namespace Report.Models
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class Deca : DbContext
	{
		public Deca()
			: base("name=Deca")
		{
		}

		public virtual DbSet<AccountingDocument> AccountingDocuments { get; set; }
		public virtual DbSet<AccountingDocumentDetail> AccountingDocumentDetails { get; set; }
		public virtual DbSet<AccountingDocumentType> AccountingDocumentTypes { get; set; }
		public virtual DbSet<AccountingYear> AccountingYears { get; set; }
		public virtual DbSet<AccountKind> AccountKinds { get; set; }
		public virtual DbSet<AccountNature> AccountNatures { get; set; }
		public virtual DbSet<Center> Centers { get; set; }
		public virtual DbSet<CertainAccount> CertainAccounts { get; set; }
		public virtual DbSet<CertainAccountType> CertainAccountTypes { get; set; }
		public virtual DbSet<City> Cities { get; set; }
		public virtual DbSet<Country> Countries { get; set; }
		public virtual DbSet<Currency> Currencies { get; set; }
		public virtual DbSet<DetailedAccount> DetailedAccounts { get; set; }
		public virtual DbSet<GroupAccount> GroupAccounts { get; set; }
		public virtual DbSet<GroupAccountType> GroupAccountTypes { get; set; }
		public virtual DbSet<TotalAccount> TotalAccounts { get; set; }
		public virtual DbSet<TotalAccountType> TotalAccountTypes { get; set; }
		public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
		public virtual DbSet<TBL_Application> TBL_Application { get; set; }
		public virtual DbSet<TBL_Application_REL_Resource> TBL_Application_REL_Resource { get; set; }
		public virtual DbSet<TBL_ApplicationIPValid> TBL_ApplicationIPValid { get; set; }
		public virtual DbSet<TBL_ErrorMessages> TBL_ErrorMessages { get; set; }
		public virtual DbSet<TBL_Language> TBL_Language { get; set; }
		public virtual DbSet<TBL_Lookup_ResourceType> TBL_Lookup_ResourceType { get; set; }
		public virtual DbSet<TBL_Messages> TBL_Messages { get; set; }
		public virtual DbSet<TBL_Resource> TBL_Resource { get; set; }
		public virtual DbSet<TBL_Lookup_OrganizationPositionType> TBL_Lookup_OrganizationPositionType { get; set; }
		public virtual DbSet<TBL_OrganizationPosition> TBL_OrganizationPosition { get; set; }
		public virtual DbSet<TBL_OrganizationUnit> TBL_OrganizationUnit { get; set; }
		public virtual DbSet<TBL_Person> TBL_Person { get; set; }
		public virtual DbSet<TBL_PersonOrganizationPosition> TBL_PersonOrganizationPosition { get; set; }
		public virtual DbSet<TBL_ApplicationActiveTime> TBL_ApplicationActiveTime { get; set; }
		public virtual DbSet<TBL_Lookup_AuditType> TBL_Lookup_AuditType { get; set; }
		public virtual DbSet<TBL_Lookup_DataAccessLevelType> TBL_Lookup_DataAccessLevelType { get; set; }
		public virtual DbSet<TBL_ResourceAuditType> TBL_ResourceAuditType { get; set; }
		public virtual DbSet<TBL_ResourceTypeAuditType> TBL_ResourceTypeAuditType { get; set; }
		public virtual DbSet<TBL_Role> TBL_Role { get; set; }
		public virtual DbSet<TBL_Role_Resource_Auditing> TBL_Role_Resource_Auditing { get; set; }
		public virtual DbSet<TBL_RoleAccessToResource> TBL_RoleAccessToResource { get; set; }
		public virtual DbSet<TBL_Token> TBL_Token { get; set; }
		public virtual DbSet<TBL_User> TBL_User { get; set; }
		public virtual DbSet<TBL_User_Resource_Auditing> TBL_User_Resource_Auditing { get; set; }
		public virtual DbSet<TBL_UserAccessToResource> TBL_UserAccessToResource { get; set; }
		public virtual DbSet<TBL_UserAccessToResourceData> TBL_UserAccessToResourceData { get; set; }
		public virtual DbSet<TBL_UserActiveTime> TBL_UserActiveTime { get; set; }
		public virtual DbSet<TBL_UserRole> TBL_UserRole { get; set; }
		public virtual DbSet<TBL_Configuration> TBL_Configuration { get; set; }
		public virtual DbSet<View_CertainAccountSelect> View_CertainAccountSelect { get; set; }
		public virtual DbSet<DataTypeView> DataTypeViews { get; set; }
		public virtual DbSet<RM_columnMetadata> RM_columnMetadata { get; set; }
		public virtual DbSet<RM_GetForeignKey> RM_GetForeignKey { get; set; }
		public virtual DbSet<RM_GetPrimaryKey> RM_GetPrimaryKey { get; set; }
		public virtual DbSet<RM_TableDescription> RM_TableDescription { get; set; }
		public virtual DbSet<RM_TableFieldsInfo> RM_TableFieldsInfo { get; set; }
		public virtual DbSet<RM_TableViewFieldsInfo> RM_TableViewFieldsInfo { get; set; }
		public virtual DbSet<RM_TableViewFieldsInfo_WithoutFarsiTitle> RM_TableViewFieldsInfo_WithoutFarsiTitle { get; set; }
		public virtual DbSet<RM_ViewFeildsInfo> RM_ViewFeildsInfo { get; set; }
		public virtual DbSet<RM_ViewFieldsInfo> RM_ViewFieldsInfo { get; set; }
		public virtual DbSet<VW_AllTablesRelationsInfo> VW_AllTablesRelationsInfo { get; set; }
		public virtual DbSet<VW_ApplicationResources> VW_ApplicationResources { get; set; }
		public virtual DbSet<VW_DataBaseResource> VW_DataBaseResource { get; set; }
		public virtual DbSet<VW_Resource> VW_Resource { get; set; }
		public virtual DbSet<VW_RM_GetForeignKey> VW_RM_GetForeignKey { get; set; }
		public virtual DbSet<VW_RM_SystemTables> VW_RM_SystemTables { get; set; }
		public virtual DbSet<VW_SystemColumns> VW_SystemColumns { get; set; }
		public virtual DbSet<VW_SystemTables> VW_SystemTables { get; set; }
		public virtual DbSet<VW_SystemTypes> VW_SystemTypes { get; set; }
		public virtual DbSet<VW_SystemViews> VW_SystemViews { get; set; }
		public virtual DbSet<VW_TableFieldInfoView> VW_TableFieldInfoView { get; set; }
		public virtual DbSet<VW_TablesRelationInfo> VW_TablesRelationInfo { get; set; }
		public virtual DbSet<VW_TablesViews> VW_TablesViews { get; set; }
		public virtual DbSet<VW_M2M_PersonOrganizationPosition> VW_M2M_PersonOrganizationPosition { get; set; }
		public virtual DbSet<VW_UserAccessToMenu> VW_UserAccessToMenu { get; set; }
		public virtual DbSet<VW_M2M_ResourceAuditType> VW_M2M_ResourceAuditType { get; set; }
		public virtual DbSet<VW_M2M_User_REL_Role> VW_M2M_User_REL_Role { get; set; }
		public virtual DbSet<VW_User_Organization> VW_User_Organization { get; set; }
		public virtual DbSet<VW_UserAccessToResource> VW_UserAccessToResource { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AccountingDocument>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<AccountingDocument>()
				.HasMany(e => e.AccountingDocumentDetails)
				.WithRequired(e => e.AccountingDocument)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AccountingDocumentDetail>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<AccountingDocumentType>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<AccountingYear>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<AccountingYear>()
				.HasMany(e => e.CertainAccounts)
				.WithRequired(e => e.AccountingYear)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AccountingYear>()
				.HasMany(e => e.DetailedAccounts)
				.WithRequired(e => e.AccountingYear)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AccountingYear>()
				.HasMany(e => e.GroupAccounts)
				.WithRequired(e => e.AccountingYear)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AccountingYear>()
				.HasMany(e => e.TotalAccounts)
				.WithRequired(e => e.AccountingYear)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AccountKind>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<AccountKind>()
				.HasMany(e => e.DetailedAccounts)
				.WithRequired(e => e.AccountKind)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AccountNature>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<AccountNature>()
				.HasMany(e => e.GroupAccounts)
				.WithRequired(e => e.AccountNature)
				.HasForeignKey(e => e.NatureId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Center>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<CertainAccount>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<CertainAccount>()
				.HasMany(e => e.CertainAccount1)
				.WithOptional(e => e.CertainAccount2)
				.HasForeignKey(e => e.ParentCertainAccountID);

			modelBuilder.Entity<CertainAccount>()
				.HasMany(e => e.DetailedAccounts)
				.WithMany(e => e.CertainAccounts)
				.Map(m => m.ToTable("DetailedAccountCertainAccount", "Accounting"));

			modelBuilder.Entity<CertainAccountType>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<City>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<Country>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<Country>()
				.HasMany(e => e.Cities)
				.WithRequired(e => e.Country)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Currency>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<DetailedAccount>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<DetailedAccount>()
				.HasMany(e => e.AccountingDocumentDetails)
				.WithRequired(e => e.DetailedAccount)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<DetailedAccount>()
				.HasMany(e => e.DetailedAccount1)
				.WithOptional(e => e.DetailedAccount2)
				.HasForeignKey(e => e.ParentDetailedAccountID);

			modelBuilder.Entity<GroupAccount>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<GroupAccount>()
				.HasMany(e => e.TotalAccounts)
				.WithRequired(e => e.GroupAccount)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<GroupAccountType>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<GroupAccountType>()
				.HasMany(e => e.GroupAccounts)
				.WithRequired(e => e.GroupAccountType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TotalAccount>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TotalAccount>()
				.HasMany(e => e.CertainAccounts)
				.WithRequired(e => e.TotalAccount)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TotalAccountType>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TotalAccountType>()
				.HasMany(e => e.TotalAccounts)
				.WithRequired(e => e.TotalAccountType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Application>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Application>()
				.HasMany(e => e.TBL_Application_REL_Resource)
				.WithRequired(e => e.TBL_Application)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Application>()
				.HasMany(e => e.TBL_ApplicationActiveTime)
				.WithRequired(e => e.TBL_Application)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Application>()
				.HasMany(e => e.TBL_ApplicationIPValid)
				.WithRequired(e => e.TBL_Application)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Application_REL_Resource>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_ApplicationIPValid>()
				.Property(e => e.IP)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<TBL_ApplicationIPValid>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_ErrorMessages>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Language>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Language>()
				.HasMany(e => e.TBL_Messages)
				.WithOptional(e => e.TBL_Language)
				.HasForeignKey(e => e.LanguageID);

			modelBuilder.Entity<TBL_Lookup_ResourceType>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Lookup_ResourceType>()
				.HasMany(e => e.TBL_Resource)
				.WithRequired(e => e.TBL_Lookup_ResourceType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Lookup_ResourceType>()
				.HasMany(e => e.TBL_ResourceTypeAuditType)
				.WithRequired(e => e.TBL_Lookup_ResourceType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Messages>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Resource>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Resource>()
				.HasMany(e => e.TBL_ResourceAuditType)
				.WithRequired(e => e.TBL_Resource)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Resource>()
				.HasMany(e => e.TBL_Resource1)
				.WithOptional(e => e.TBL_Resource2)
				.HasForeignKey(e => e.ParentResourceID);

			modelBuilder.Entity<TBL_Resource>()
				.HasMany(e => e.TBL_RoleAccessToResource)
				.WithRequired(e => e.TBL_Resource)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Resource>()
				.HasMany(e => e.TBL_Role_Resource_Auditing)
				.WithRequired(e => e.TBL_Resource)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Resource>()
				.HasMany(e => e.TBL_UserAccessToResource)
				.WithRequired(e => e.TBL_Resource)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Resource>()
				.HasMany(e => e.TBL_UserAccessToResourceData)
				.WithRequired(e => e.TBL_Resource)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Resource>()
				.HasMany(e => e.TBL_User_Resource_Auditing)
				.WithRequired(e => e.TBL_Resource)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Lookup_OrganizationPositionType>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Lookup_OrganizationPositionType>()
				.HasMany(e => e.TBL_OrganizationPosition)
				.WithRequired(e => e.TBL_Lookup_OrganizationPositionType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_OrganizationPosition>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_OrganizationPosition>()
				.HasMany(e => e.TBL_PersonOrganizationPosition)
				.WithRequired(e => e.TBL_OrganizationPosition)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_OrganizationPosition>()
				.HasMany(e => e.TBL_Token)
				.WithRequired(e => e.TBL_OrganizationPosition)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_OrganizationUnit>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_OrganizationUnit>()
				.HasMany(e => e.AccountingYears)
				.WithRequired(e => e.TBL_OrganizationUnit)
				.HasForeignKey(e => e.OrganizationId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_OrganizationUnit>()
				.HasMany(e => e.TBL_OrganizationPosition)
				.WithRequired(e => e.TBL_OrganizationUnit)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_OrganizationUnit>()
				.HasMany(e => e.TBL_OrganizationUnit1)
				.WithOptional(e => e.TBL_OrganizationUnit2)
				.HasForeignKey(e => e.ParentOrganizationUnitID);

			modelBuilder.Entity<TBL_Person>()
				.Property(e => e.NationalCode)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<TBL_Person>()
				.Property(e => e.NationalID)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<TBL_Person>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Person>()
				.HasMany(e => e.TBL_PersonOrganizationPosition)
				.WithRequired(e => e.TBL_Person)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_PersonOrganizationPosition>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_ApplicationActiveTime>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Lookup_AuditType>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Lookup_AuditType>()
				.HasMany(e => e.TBL_ResourceAuditType)
				.WithRequired(e => e.TBL_Lookup_AuditType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Lookup_AuditType>()
				.HasMany(e => e.TBL_ResourceTypeAuditType)
				.WithRequired(e => e.TBL_Lookup_AuditType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Lookup_DataAccessLevelType>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_ResourceAuditType>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_ResourceTypeAuditType>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Role>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Role>()
				.HasMany(e => e.TBL_RoleAccessToResource)
				.WithRequired(e => e.TBL_Role)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Role>()
				.HasMany(e => e.TBL_Role_Resource_Auditing)
				.WithRequired(e => e.TBL_Role)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Role>()
				.HasMany(e => e.TBL_UserRole)
				.WithRequired(e => e.TBL_Role)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_Role_Resource_Auditing>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_RoleAccessToResource>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Token>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_User>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_User>()
				.HasMany(e => e.TBL_Person)
				.WithRequired(e => e.TBL_User)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_User>()
				.HasMany(e => e.TBL_Token)
				.WithRequired(e => e.TBL_User)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_User>()
				.HasMany(e => e.TBL_UserRole)
				.WithRequired(e => e.TBL_User)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_User>()
				.HasMany(e => e.TBL_UserAccessToResource)
				.WithRequired(e => e.TBL_User)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_User>()
				.HasMany(e => e.TBL_UserActiveTime)
				.WithRequired(e => e.TBL_User)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TBL_User_Resource_Auditing>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_UserAccessToResource>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_UserAccessToResourceData>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_UserActiveTime>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_UserRole>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<TBL_Configuration>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<VW_M2M_PersonOrganizationPosition>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<VW_M2M_ResourceAuditType>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();

			modelBuilder.Entity<VW_M2M_User_REL_Role>()
				.Property(e => e.ActionTimeStamp)
				.IsFixedLength();
		}
	}
}
