from __future__ import print_function
from Calf_Counter import *
from sqltest import *
import json

import pprint
total_study_days = 24
contacts=[]
# big loop through all 2.5 ish seconds -- most likely a nested loop
# when seconds = 86400 -- day++
calf_list = create_dict(total_study_days)
#print(calf_list[170]['sick'])
# i = 101
#
# print (len(calf_list))
# assert len(calf_list) == 70


#imports the csv file that contains shedding data
calf_list = health_status(calf_list)

window = 60*60 # window for grouping time default = 30 secs
frames = 86400/window #this gives total time frams in a day based on the window chosen

start = 1462597200 #  2016-05-07 00:00:00
end = 1464335999 #  2016-05-27 02:59:59 AM
index = start
day = 0

while day < total_study_days: #24:
    #day = (index - start) % 86400
    groupings = 0
    #loop through the frames
    while groupings < frames: 
        # returns a list of pairs of contacts
        contacts = pull_data(index)

        index += window
        #increment the appropriate counts
        add_counts(contacts, day)

        groupings +=1
        
    day +=1
export_data()
