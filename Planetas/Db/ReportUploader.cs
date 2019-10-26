using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Planetas
{
    public class ReportUploader
    {
        WeatherReport report;

        public ReportUploader(WeatherReport report)
        {
            this.report = report;
            InitMappings();
        }

        private void InitMappings()
        {
            BsonClassMap.RegisterClassMap<WeatherReport>(cm =>
            {
                cm.MapMember(c => c.DraughtDays);
                cm.MapMember(c => c.MaxIntensityDay);
                cm.MapMember(c => c.MaxRainIntensity);
                cm.MapMember(c => c.RainDays);
                cm.MapMember(c => c.OptimumDays);
            });

            BsonClassMap.RegisterClassMap<DayReport>(cm =>
            {
                cm.MapMember(c => c.Day);
                cm.MapMember(c => c.RainIntensity);
                cm.MapMember(c => c.Weather).SetSerializer(new EnumSerializer<Weather>(BsonType.String));
            });
        }

        public void Upload()
        {
            var client = new MongoClient("mongodb+srv://meli-user:zLnlBqEFiUwlZzzG@meli-solarsystem-jcs7m.mongodb.net/test?retryWrites=true&w=majority");
            var db = client.GetDatabase("test");

            UploadSummary(db);
            UploadDays(db);
        }

        private void UploadSummary(IMongoDatabase db)
        {
            var col = db.GetCollection<WeatherReport>("Summary");

            //Delete all documents
            col.DeleteMany(doc => true);

            col.InsertOne(report);
        }

        private void UploadDays(IMongoDatabase db)
        {
            var col = db.GetCollection<DayReport>("Days");

            col.DeleteMany(doc => true);

            col.InsertMany(report.WeatherPerDay);
        }
    }
}
