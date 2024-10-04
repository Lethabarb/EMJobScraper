namespace Common.Models.NT
{
    public class Advertisement
    {
        public int? rtfid { get; set; }
        public string? positionNumber { get; set; }
        public string? jobTitle { get; set; }
        public string? vacancyType { get; set; }
        public string? altVacancyType { get; set; }
        public string? numberOfVacancies { get; set; }
        public string? vacancyTypeCodeValue { get; set; }
        public string? agency { get; set; }
        public string? section { get; set; }
        public string? contactPerson { get; set; }
        public string? primaryObjective { get; set; }
        public string? advertisingType { get; set; }
        public string? applicationType { get; set; }
        public string? specialInstructions { get; set; }
        public string? closingDate { get; set; }
        public string? locations { get; set; }
        public string? designations { get; set; }
        public bool? isCanceled { get; set; }
        public bool? offlineApplicationsOnlyFlag { get; set; }
        public string? vacancyDuration { get; set; }
        public string? vacancyEndDate { get; set; }
        public string? dateAdded { get; set; }
        public int? ApplicationFormId { get; set; }
        public int? RecruitmentProgramId { get; set; }
        public string? CloseDateClause { get; set; }
        public bool? isSaved { get; set; }
        public bool? viewed { get; set; }
        public string? RecruitmentProgramUrl { get; set; }
        public string? closingDateAsDateTime { get; set; }
        public double lowestRemuneration { get; set; }
        public double highestRemuneration { get; set; }
        public List<VacancyDesignation>? vacancyDesignationList { get; set; }
        public List<Attachment>? attachmentsList { get; set; }
        public string? headingLabelText { get; set; }
        public bool? canApplyOnline { get; set; }
        public bool? hasSpecialInstructions { get; set; }
        public string? endDateDurationDetail { get; set; }
        public string? endDateDurationLabel { get; set; }
        public bool? isTempOrCasual { get; set; }
        public string? formattedClosingDate { get; set; }
        public bool? hasRecruitmentProgramUrl { get; set; }
        public string? getFormattedDesignationList { get; set; }
    }
}
