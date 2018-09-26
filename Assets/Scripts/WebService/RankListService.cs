﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// 此源代码由 wsdl 自动生成, Version=4.6.1055.0。
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="RankListServiceSoap", Namespace="http://tempuri.org/")]
public partial class RankListService : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback upLoadDataOperationCompleted;
    
    private System.Threading.SendOrPostCallback getRankListOperationCompleted;
    
    /// <remarks/>
    public RankListService() {
        this.Url = "http://47.75.2.153/RankListService.asmx";
    }
    
    /// <remarks/>
    public event upLoadDataCompletedEventHandler upLoadDataCompleted;
    
    /// <remarks/>
    public event getRankListCompletedEventHandler getRankListCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/upLoadData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void upLoadData(string MapName, string UserName, int CostTime, int Score) {
        this.Invoke("upLoadData", new object[] {
                    MapName,
                    UserName,
                    CostTime,
                    Score});
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginupLoadData(string MapName, string UserName, int CostTime, int Score, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("upLoadData", new object[] {
                    MapName,
                    UserName,
                    CostTime,
                    Score}, callback, asyncState);
    }
    
    /// <remarks/>
    public void EndupLoadData(System.IAsyncResult asyncResult) {
        this.EndInvoke(asyncResult);
    }
    
    /// <remarks/>
    public void upLoadDataAsync(string MapName, string UserName, int CostTime, int Score) {
        this.upLoadDataAsync(MapName, UserName, CostTime, Score, null);
    }
    
    /// <remarks/>
    public void upLoadDataAsync(string MapName, string UserName, int CostTime, int Score, object userState) {
        if ((this.upLoadDataOperationCompleted == null)) {
            this.upLoadDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupLoadDataOperationCompleted);
        }
        this.InvokeAsync("upLoadData", new object[] {
                    MapName,
                    UserName,
                    CostTime,
                    Score}, this.upLoadDataOperationCompleted, userState);
    }
    
    private void OnupLoadDataOperationCompleted(object arg) {
        if ((this.upLoadDataCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.upLoadDataCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getRankList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
    public RL_Data[] getRankList(string MapName) {
        object[] results = this.Invoke("getRankList", new object[] {
                    MapName});
        return ((RL_Data[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetRankList(string MapName, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getRankList", new object[] {
                    MapName}, callback, asyncState);
    }
    
    /// <remarks/>
    public RL_Data[] EndgetRankList(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((RL_Data[])(results[0]));
    }
    
    /// <remarks/>
    public void getRankListAsync(string MapName) {
        this.getRankListAsync(MapName, null);
    }
    
    /// <remarks/>
    public void getRankListAsync(string MapName, object userState) {
        if ((this.getRankListOperationCompleted == null)) {
            this.getRankListOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetRankListOperationCompleted);
        }
        this.InvokeAsync("getRankList", new object[] {
                    MapName}, this.getRankListOperationCompleted, userState);
    }
    
    private void OngetRankListOperationCompleted(object arg) {
        if ((this.getRankListCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getRankListCompleted(this, new getRankListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
public partial class RL_Data {
    
    private string userNameField;
    
    private int costTimeField;
    
    private int scoreField;
    
    /// <remarks/>
    public string UserName {
        get {
            return this.userNameField;
        }
        set {
            this.userNameField = value;
        }
    }
    
    /// <remarks/>
    public int CostTime {
        get {
            return this.costTimeField;
        }
        set {
            this.costTimeField = value;
        }
    }
    
    /// <remarks/>
    public int Score {
        get {
            return this.scoreField;
        }
        set {
            this.scoreField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
public delegate void upLoadDataCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
public delegate void getRankListCompletedEventHandler(object sender, getRankListCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class getRankListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal getRankListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public RL_Data[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((RL_Data[])(this.results[0]));
        }
    }
}
