﻿using System;
using System.Net;
using OneSignal.CSharp.SDK.Serializers;
using RestSharp;

namespace OneSignal.CSharp.SDK.Resources.Notifications
{
    /// <summary>
    /// Class used to define resources needed for client to manage notifications.
    /// </summary>
    public class NotificationsResource : BaseResource, INotificationsResource
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="apiKey">Your OneSignal API key</param>
        /// <param name="apiUri">API uri (https://onesignal.com/api/v1/notifications)</param>
        public NotificationsResource(string apiKey, string apiUri) : base(apiKey, apiUri)
        {
        }

        /// <summary>
        /// Creates new notification to be sent by OneSignal system.
        /// </summary>
        /// <param name="options">Options used for notification create operation.</param>
        /// <returns></returns>
        public NotificationCreateResult Create(NotificationCreateOptions options)
        {
            RestRequest restRequest = new RestRequest("notifications", Method.POST);

            restRequest.AddHeader("Authorization", string.Format("Basic {0}", base.ApiKey));

            restRequest.RequestFormat = DataFormat.Json;
            restRequest.JsonSerializer = new NewtonsoftJsonSerializer();
            restRequest.AddBody(options);

            IRestResponse<NotificationCreateResult> restResponse = base.RestClient.Execute<NotificationCreateResult>(restRequest);

            if (restResponse.ErrorException != null)
            {
                throw restResponse.ErrorException;
            }
            else if (restResponse.StatusCode != HttpStatusCode.OK && restResponse.Content != null)
            {
                throw new Exception(restResponse.Content);
            }

            return restResponse.Data;
        }

        /// <summary>
        /// Get delivery and convert report about single notification.
        /// </summary>
        /// <param name="options">Options used for getting delivery and convert report about single notification.</param>
        /// <returns></returns>
        public NotificationViewResult View(NotificationViewOptions options)
        {
            var baseRequestPath = "notifications/{0}?app_id={1}";
            
            RestRequest restRequest = new RestRequest(string.Format(baseRequestPath, options.Id, options.AppId), Method.GET);

            restRequest.RequestFormat = DataFormat.Json;
            restRequest.JsonSerializer = new NewtonsoftJsonSerializer();

            IRestResponse<NotificationViewResult> restResponse = base.RestClient.Execute<NotificationViewResult>(restRequest);

            if (restResponse.ErrorException != null)
            {
                throw restResponse.ErrorException;
            }
            else if (restResponse.StatusCode != HttpStatusCode.OK && restResponse.Content != null)
            {
                throw new Exception(restResponse.Content);
            }

            return restResponse.Data;
        }
    }
}
