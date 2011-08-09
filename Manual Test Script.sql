select * from tbRPT_InactiveSites

select * from tbRPT_DailyActivityGP (nolock)

select * from tbEPS_Organisation (nolock)
sp_help tbEPS_Organisation

-- HB1 

insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(1, '00001', 7, null, 'Test GP 1', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS1', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(2, '00002', 7, null, 'Test GP 2', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS2', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(3, '00003', 7, null, 'Test GP 3', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS3', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(4, '00004', 7, null, 'Test GP 4', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS4', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(5, '00005', 7, null, 'Test GP 5', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS5', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(6, '00006', 7, null, 'Test GP 6', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS6', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(7, '00007', 7, null, 'Test GP 7', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS7', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(8, '00008', 7, null, 'Test GP 8', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS8', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(9, '00009', 7, null, 'Test GP 9', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS9', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(10, '00010', 7, null, 'Test GP 10', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS10', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(11, '00011', 7, null, 'Test GP 11', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS11', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(12, '00012', 7, null, 'Test GP 12', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS12', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(13, '00013', 7, null, 'Test GP 13', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS13', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(14, '00014', 7, null, 'Test GP 14', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS14', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(15, '00015', 7, null, 'Test GP 15', 'Highland Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS15', 0, null)

-- HB 2

insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(16, '00016', 7, null, 'Test GP 16', 'Tayside Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS16', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(17, '00017', 7, null, 'Test GP 17', 'Tayside Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS17', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(18, '00018', 7, null, 'Test GP 18', 'Tayside Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS18', 0, null)
insert into tbEPS_Organisation (rid, id, organisationTypeRid, shortName, name, healthBoardName, alternateName, prsRefreshEnabled, epsServicesEnabled,
								startDate, endDate, archived, auditCreatedOn, auditCreatedBy, auditUpdatedOn, auditUpdatedBy, address1, address2,
								address3, address4, postCode, country, telephone, fax, email, supplier, supplierReference, dispensing, notes)
						values	(19, '00019', 7, null, 'Test GP 19', 'Tayside Healthboard', null, 1, 1, '2011-01-01', null, 0, '2011-01-01 00:00:00.000',
								'eric', null, null, null, null, null, null, null, null, null, null, null, 'INPS', 'INPS19', 0, null)
								
update tbEPS_Organisation
-- set healthBoardName = 'Highland Healthboard'
set supplier = 'EMIS',
	supplierReference = 'EMIS1'
where rid = 19


select * from tbRPT_DailyActivityGP (nolock)
delete tbRPT_DailyActivityGP
sp_help tbRPT_DailyActivityGP

update tbRPT_DailyActivityGP
set date = '2011-08-02'

insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00001', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00002', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00003', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00004', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00005', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00006', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00007', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00008', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00009', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00010', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00011', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00012', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00013', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00014', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00015', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00016', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00017', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00018', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
insert into tbRPT_DailyActivityGP (org, date, amsPrescriptions, amsCancellations, amsAmendments, cmsPrescriptions, cmsCancellations, cmsUpdatesRequests,
									cmsTreatmentUpdates, cmsComplianceUpdates, gpRegistrationUpdatesRequests)
							values('00019', getdate(), 0, 0, 0, 0, 0, 0, 0, 0, 0)
							
							
select * from tbRPT_OrgSupplier
sp_help tbRPT_OrgSupplier
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00001', '51000001', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00002', '51000002', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00003', '51000003', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00004', '51000004', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00005', '51000005', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00006', '51000006', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00007', '51000007', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00008', '51000008', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00009', '51000009', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00010', '51000010', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00011', '51000011', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00012', '51000012', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00013', '51000013', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00014', '51000014', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00015', '51000015', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00016', '51000016', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00017', '51000017', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00018', '51000018', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
insert into tbRPT_OrgSupplier (org, epoc, supplier, product, version, latestMsg, X509SerialNumber, downloaddate, AuthCertSerialNumber, AuthCertDownloadDate, 
								ipAddress, reportingSupplier, disp, extended, previousSupplier, previousProduct, CmsBetaSite, EpocCertBy, ResignCertBy,
								latestAMS, latestCMS, latestMAS)
						values('00019', '51000019', 'INPS', null, null, GETDATE()-14, null, null, null, null, null, null, 0, null, null, null, null,
								null, null, GETDATE()-14, null, null)
								
								
update tbRPT_OrgSupplier
set supplier = 'EMIS'
where org = '00019'

select * from tbRPT_SupplierContacts
sp_help tbRPT_SupplierContacts

insert into tbRPT_SupplierContacts (Rid, Supplier, Contact) values (1, 'EMIS', 'eric.blair@atos.net')
insert into tbRPT_SupplierContacts (Rid, Supplier, Contact) values (1, 'INPS', 'eric.blair@atos.net')

select * from tbRPT_HealthBoardContacts
sp_help tbRPT_HealthBoardContacts

insert into tbRPT_HealthBoardContacts (Rid, HealthBoard, Contact) values (1, 'Highland Healthboard', 'eric.blair@atos.net')
insert into tbRPT_HealthBoardContacts (Rid, HealthBoard, Contact) values (1, 'Tayside Healthboard', 'eric.blair@atos.net')


delete from tbRPT_SupplierContacts
where Supplier like '%Healthboard'

delete tbRPT_InactiveSites