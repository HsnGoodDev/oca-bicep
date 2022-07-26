using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace oca.model;

public class SabangnetProduct {
    public string gdsCd {get; set;}
    public string gdsNm {get; set;}
    public string gdsSclsCd {get; set;}
    public string brndCd {get; set;}
    public string mfgNatnCd {get; set;}
    public string mdEmpNo {get; set;}
    public string renewGdsYn {get; set;}
    public string newprdSpCd {get; set;}
    public string manBabySpCd {get; set;}
    public string ebGdsYn {get; set;}
    public string buyTypCd {get; set;}
    public string feeStdrdCd {get; set;}
    public double feeStdrdAmt {get; set;}
    public double genFeeRt {get; set;}
    public double cardFeeRt {get; set;}
    public string vatSpCd {get; set;}
    public string suplrCd {get; set;}
    public string mvndrYn {get; set;}
    public string dstbtrCd {get; set;}
    public string mvndrCd {get; set;}
    public string infnSelImpsYn {get; set;}
    public string dcApplyPsblYn {get; set;}
    public string mblGftcrdSttlPsblYn {get; set;}
    public string empDcYn {get; set;}
    public string coopCardDcYn {get; set;}
    public string pntAccmYn {get; set;}
    public string medapYn {get; set;}
}